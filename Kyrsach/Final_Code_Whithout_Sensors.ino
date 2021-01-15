// WiFi
#include <WiFi.h>
#include <WiFiClient.h>
WiFiClient client;
#include <Wire.h>
#include <OneWire.h>
#include <ArduinoJson.h>

// Sensor
#define sensorCount 3
char* sensorName[] = {"IP18131101TEMP","Led","Servo_door"};
float sensorValues[sensorCount];
int last_door = 0;
int start_temp = 0;

// Name Sensor
#define  IP18131101TEMP 0
#define  Led 1
#define  Servo_door 2

// WiFi config
char ssid[] = "Galaxy S10b57e";
char pass[] = "1234567890";

// ThingWorx
char iot_server[] = "192.168.43.36";
IPAddress iot_address(192,168,43,36);
char appKey[] = "e4f6bf91-5c87-4dae-ad36-a4a8069d21e0";
char thingName[] = "Kyrsovay";
char serviceName[] = "Code";

// Timer
long timer_iot_timeout = 0;
#define TIMEOUT 1000 // 1 second timout
#define IOT_TIMEOUT1 5000
#define IOT_TIMEOUT2 100

#define BUFF_LENGTH 256
char buff[BUFF_LENGTH] = "";

void setup() {
  Serial.begin(115200);
  Serial.println("Получение первого показателя датчика");
  start_temp = random(27);
  sensorValues[IP18131101TEMP] = start_temp;
  Serial.println(sensorValues[IP18131101TEMP]);
  
  Serial.println("Conecting to WiFi");
  WiFi.begin(ssid, pass);
  while (WiFi.status() != WL_CONNECTED){
    delay(500);
    Serial.println(".");
    }
  Serial.println("Local IP:");
  Serial.println(WiFi.localIP());
}

void GetTemp(){
  sensorValues[IP18131101TEMP] = start_temp - (last_door - sensorValues[Servo_door])/2;
  if (sensorValues[IP18131101TEMP] < 0){
    sensorValues[IP18131101TEMP] = 0;
  }
}

void printData()
{
  Serial.println("Обработанные данные:");
  Serial.println("T=" + String(sensorValues[IP18131101TEMP]) + "*C");
  Serial.println("Servo_door = " + String(sensorValues[Servo_door]) + " grad");
  String color = "Blue";
  if (sensorValues[Led] == 1){ color = "Yellow";}
  else if (sensorValues[Led] == 2){ color = "Green";} 
  else if (sensorValues[Led] == 3){ color = "Red";} 
  Serial.println("Led = " + color);
  Serial.println();
}

void sendThingWorxStream()
{
  // Подключение к серверу
  Serial.println("Connecting to IoT server...");
  if (client.connect(iot_address, 80))
  {
    // Проверка установления соединения
    if (client.connected())
    {
      // Отправка заголовка сетевого пакета
      Serial.println("Sending data to IoT server...\n");
      Serial.print("POST /Thingworx/Things/");  client.print("POST /Thingworx/Things/");
      Serial.print(thingName);  client.print(thingName);
      Serial.print("/Services/"); client.print("/Services/");
      Serial.print(serviceName); client.print(serviceName);
      Serial.print("?appKey="); client.print("?appKey=");
      Serial.print(appKey);  client.print(appKey);
      Serial.print("&method=post&x-thingworx-session=true");  client.print("&method=post&x-thingworx-session=true");
      
      // Отправка данных с датчиков
      Serial.println();
      Serial.println("Отправляем данные:");
      Serial.print("&"); client.print("&");Serial.print(sensorName[0]);  client.print(sensorName[0]);
      Serial.print("=");  client.print("=");Serial.print(sensorValues[0]); client.print(sensorValues[0]);
      Serial.print("&"); client.print("&");Serial.print(sensorName[2]);  client.print(sensorName[2]);
      Serial.print("=");  client.print("=");Serial.print(sensorValues[2]); client.print(sensorValues[2]);
      Serial.println();
      client.println(" HTTP/1.1");  client.println("Accept: application/json");   client.print("Host: ");   client.println(iot_server);   client.println("Content-Type: application/json");   client.println();
      
      // Ждем ответа от сервера
      timer_iot_timeout = millis();
      while ((client.available() == 0) && (millis() < timer_iot_timeout + IOT_TIMEOUT1)) {delay(10);}

      // Выводим ответ о сервера, и, если медленное соединение, ждем выход по таймауту
      int iii = 0;
      bool currentLineIsBlank = true;
      bool flagJSON = false;
      timer_iot_timeout = millis();
      while ((millis() < timer_iot_timeout + IOT_TIMEOUT2) && (client.connected()))
      {
        while (client.available() > 0)
        {
          char symb = client.read();
          //Serial.print(symb);
          if (symb == '{')
          {
            flagJSON = true;
          }
          else if (symb == '}')
          {
            flagJSON = false;
          }
          if (flagJSON == true)
          {
            buff[iii] = symb;
            iii ++;
          }
          delay(10);
          timer_iot_timeout = millis();
        }
        delay(10);
      }
      buff[iii] = '}';
      buff[iii + 1] = '\0';
      
      //Serial.println(buff);
      StaticJsonBuffer<BUFF_LENGTH> jsonBuffer;
      JsonObject& json_array = jsonBuffer.parseObject(buff);
      sensorValues[Led] = json_array["Led"];
      sensorValues[Servo_door] = json_array["Servo_door"];
      // Закрываем соединение
      client.stop();
    }
  }
}
void loop() {
  GetTemp();
  sendThingWorxStream();
  printData();
  Serial.println("/////////////////////////////////////////////////////////////////////////////");
  delay(1000);
}
