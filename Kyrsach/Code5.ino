// WiFi
#include <WiFi.h>
#include <WiFiClient.h>
WiFiClient client;
#include <Wire.h>
#include <OneWire.h>
#include <ArduinoJson.h>

// Sensor
//#include <Adafruit_Sensor.h>
#include <DHT.h>
#define DHTPIN            15         //  Контакт, который подключен к датчику DHT
DHT dht(DHTPIN, DHT11);
int t  = random(40);

// Display
#include <LiquidCrystal_I2C.h>
LiquidCrystal_I2C lcd(0x27,16,2);

// Sensor
#define sensorCount 3
char* sensorName[] = {"Temp","Led","Servo_door"};
float sensorValues[sensorCount];

// Name Sensor
#define  Temp 0
#define  Led 1
#define  Servo_door 2

// Led
#define Red 13
#define Green 12
#define Yellow 14
#define Blue 27

// Servo
#include <ESP32Servo.h>
#define SERVOPIN 17
Servo myservo;  

// WiFi config
char ssid[] = "Galaxy S10b57e";
char pass[] = "1234567890";

// ThingWorx
char iot_server[] = "192.168.43.36";
IPAddress iot_address(192,168,43,36);
char appKey[] = "30bf4fe9-5d55-4ddb-990a-3cdcbdfa8fb2";
char thingName[] = "Kyrsovay";
char serviceName[] = "Code";

// Timer
long timer_iot_timeout = 0;
#define TIMEOUT 1000 // 1 second timout
#define IOT_TIMEOUT1 5000
#define IOT_TIMEOUT2 100
#define AUTO_UPDATE_TIME 500
#define THINGWORX_UPDATE_TIME 30000
#define SENSORS_UPDATE_TIME 5000
#define PRINT_UPDATE_TIME 5000
unsigned long timer_thingworx = 0;
unsigned long timer_print = 0;
unsigned long timer_auto = 0;
unsigned long timer_sensors = 0;

#define BUFF_LENGTH 256
char buff[BUFF_LENGTH] = "";
int btn_state = 0;
int auto_control = 0;

void setup() {
  Serial.begin(115200);
  dht.begin();
  myservo.attach(SERVOPIN);

  Serial.println("Conecting to WiFi");
  WiFi.begin(ssid, pass);
  while (WiFi.status() != WL_CONNECTED){
    delay(500);
    Serial.println(".");
    }
  Serial.println("Local IP:");
  Serial.println(WiFi.localIP());
  
  pinMode(Red, OUTPUT); 
  pinMode(Green, OUTPUT); 
  pinMode(Yellow, OUTPUT);
  pinMode(Blue, OUTPUT);

  lcd.init();
  lcd.backlight();
  lcd.clear();
  lcd.setCursor(0,0);
}

void sensor(){
  sensorValues[Temp] = t;
  if (isnan(sensorValues[Temp]))
  {
    Serial.println("Failed to read from DHT11 sensor!");
  }
  if (sensorValues[Temp] > 28){
    t -= (sensorValues[Temp] - 28);
    sensorValues[Led] = 3;
    sensorValues[Servo_door] -= (sensorValues[Temp] - 28);
    myservo.write(sensorValues[Servo_door]);
    digitalWrite(Green, LOW);  
    digitalWrite(Red, HIGH);
    digitalWrite(Yellow, LOW);  
    digitalWrite(Blue, LOW);
  }
  else if (sensorValues[Temp] >= 24){
    sensorValues[Led] = 2;
    digitalWrite(Green, HIGH);  
    digitalWrite(Red, LOW);
    digitalWrite(Yellow, LOW);  
    digitalWrite(Blue, LOW);
  }
  else if (sensorValues[Temp] > 14) {
    sensorValues[Led] = 1;
     t += 10;
    sensorValues[Servo_door] += 10;
    myservo.write(sensorValues[Servo_door]); 
    digitalWrite(Yellow, HIGH);
    digitalWrite(Green, LOW);
    digitalWrite(Red, LOW);
    digitalWrite(Blue, LOW);

  }
  else{
    sensorValues[Led] = 0;
    t += 20;
    sensorValues[Servo_door] += 20;
    myservo.write(sensorValues[Servo_door]); 
    digitalWrite(Red, LOW);
    digitalWrite(Blue, HIGH);
    digitalWrite(Yellow, LOW);
    digitalWrite(Green, LOW);
  }
}

void printDataLCD()
{
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd_printstr("T = " + String(sensorValues[Temp]) + " *C");
  lcd.setCursor(0, 1);
  String color = "Red";
  if (sensorValues[Led] == 1){ color = "Yellow";}
  else if (sensorValues[Led] == 2){ color = "Green";} 
  lcd_printstr("Led = " + color);
  lcd.setCursor(0, 2);
  lcd_printstr("Servo_door = " + String(sensorValues[Servo_door]) + " grad");
}
void lcd_printstr(String str1)
{
  for (int i = 0; i < str1.length(); i++)
  {
    lcd.print(str1.charAt(i));
  }
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
      Serial.print("POST /Thingworx/Things/");
      client.print("POST /Thingworx/Things/");
      Serial.print(thingName);
      client.print(thingName);
      Serial.print("/Services/");
      client.print("/Services/");
      Serial.print(serviceName);
      client.print(serviceName);
      Serial.print("?appKey=");
      client.print("?appKey=");
      Serial.print(appKey);
      client.print(appKey);
      Serial.print("&method=post&x-thingworx-session=true");
      client.print("&method=post&x-thingworx-session=true");
      // Отправка данных с датчиков
      for (int idx = 0; idx < sensorCount; idx ++)
      {
        Serial.print("&");
        client.print("&");
        Serial.print(sensorName[idx]);
        client.print(sensorName[idx]);
        Serial.print("=");
        client.print("=");
        Serial.print(sensorValues[idx]);
        client.print(sensorValues[idx]);
      }
      Serial.println();
      // Закрываем пакет
      //Serial.println(" HTTP/1.1");
      client.println(" HTTP/1.1");
      //Serial.println("Accept: application/json");
      client.println("Accept: application/json");
      //Serial.print("Host: ");
      client.print("Host: ");
      //Serial.println(iot_server);
      client.println(iot_server);
      //Serial.println("Content-Type: application/json");
      client.println("Content-Type: application/json");
      //Serial.println();
      client.println();

      // Ждем ответа от сервера
      timer_iot_timeout = millis();
      while ((client.available() == 0) && (millis() < timer_iot_timeout + IOT_TIMEOUT1))
      {
        delay(10);
      }

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
      Serial.println(buff);
      // Закрываем соединение
      client.stop();
    }
  }
}

void printAllSensors()
{
  for (int i = 0; i < sensorCount; i++)
  {
    Serial.print(sensorName[i]);
    Serial.print(" = ");
    Serial.println(sensorValues[i]);
  }
}
void loop() {
  sensor();
  sendThingWorxStream();
  printDataLCD();
  printAllSensors();
  delay(1000);
}
