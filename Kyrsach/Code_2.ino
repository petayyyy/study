// WiFi
#include <WiFi.h>
#include <WiFiClient.h>
WiFiClient client;
#include <Wire.h>
#include <OneWire.h>
#include <ArduinoJson.h>

// Sensor
#include <Adafruit_Sensor.h>
#include <DHT.h>
#define DHTPIN            13         //  Контакт, который подключен к датчику DHT
DHT dht(DHTPIN, DHT11);

// Display
#include <LiquidCrystal_I2C.h>
LiquidCrystal_I2C lcd(0x27,16,2);

// Sensor
#define sensorCount 3
char* sensorName[] = {"Temp","Led","Servo_dor"};
float sensorValues[sensorCount];

// Name Sensor
#define  Temp 0
#define  Led 1
#define  Servo_dor 2

// Led
#define Red 14
#define Green 15
#define Yellow 16

// Servo
#include <ESP32Servo.h>
#define SERVOPIN 12
Servo myservo;  

// WiFi config
char ssid[] = "ПИДОРАСЫ";
char pass[] = "14881111)";

// ThingWorx
char iot_server[] = "jrskillsiot.cloud.thingworx.com";
IPAddress iot_address(52,203,26,63);
char appKey[] = "";
char thingName[] = "";
char serviceName[] = "";

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
int relay_control = 0;
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

  lcd.init();
  lcd.backlight();
  lcd.clear();
  lcd.setCursor(0,0);
}

void sensor(){
  sensorValues[Temp] = dht.readTemperature();
  if (isnan(sensorValues[Temp]))
  {
    Serial.println("Failed to read from DHT11 sensor!");
  }
  if (sensorValues[Temp] > 28){
    sensorValues[Servo_dor] -= (sensorValues[Temp] - 28);
    myservo.write(sensorValues[Servo_dor]);
  }
  else if (sensorValues[Temp] > 24){
    sensorValues[Led] = 2;
    digitalWrite(Green, HIGH);  
    digitalWrite(Red, LOW);
    digitalWrite(Yellow, LOW);  
  }
  else if (sensorValues[Temp] > 14) {
    sensorValues[Led] = 1;
    sensorValues[Servo_dor] += 10;
    myservo.write(sensorValues[Servo_dor]); 
    digitalWrite(Yellow, HIGH);
    digitalWrite(Green, LOW);
    digitalWrite(Red, LOW);
  }
  else{
    sensorValues[Led] = 0;
    sensorValues[Servo_dor] += 20;
    myservo.write(sensorValues[Servo_dor]); 
    digitalWrite(Red, HIGH);
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
  lcd_printstr("Servo_dor = " + String(sensorValues[Servo_dor]) + " grad");
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
      // Закрываем пакет
      Serial.println(" HTTP/1.1");
      client.println(" HTTP/1.1");
      Serial.println("Accept: application/json");
      client.println("Accept: application/json");
      Serial.print("Host: ");
      client.print("Host: ");
      Serial.println(iot_server);
      client.println(iot_server);
      Serial.println("Content-Type: application/json");
      client.println("Content-Type: application/json");
      Serial.println();
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
          Serial.print(symb);
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

      // Расшифровываем параметры
      StaticJsonBuffer<BUFF_LENGTH> jsonBuffer;
      JsonObject& json_array = jsonBuffer.parseObject(buff);
      relay_control = json_array["relay_control"];
      Serial.println("Relay control:   " + String(relay_control));
      Serial.println();
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
  // Send data to ThingWorx server
  if (millis() > timer_thingworx + THINGWORX_UPDATE_TIME)
  {
    sendThingWorxStream();
    timer_thingworx = millis();
  }
  // Read all sensors
  if (millis() > timer_sensors + SENSORS_UPDATE_TIME)
  {
    sensor();
    printAllSensors();
    timer_sensors = millis();
  }

  // Print data to LCD
  if (millis() > timer_print + PRINT_UPDATE_TIME)
  {
    printDataLCD();
    timer_print = millis();
  }
}
