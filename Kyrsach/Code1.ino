#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <DHT_U.h>
#define DHTPIN            13         //  Контакт, который подключен к датчику DHT
DHT_Unified dht(DHTPIN, DHT11);
uint32_t delayMS;
int sensorTemp = 0;,
int sensorLed = 0;
int pose = 0;

// Led
#define Red 14
#define Green 15
#define Yellow 16

// Servo
#define SERVOPIN 12
Servo myservo;  

void setup() {
  Serial.begin(115200);
  dht.begin();
  myservo.attach(SERVOPIN);
  
  pinMode(Red, OUTPUT); 
  pinMode(Green, OUTPUT); 
  pinMode(Yellow, OUTPUT); 
}

void sensor(){
  sensorTemp = dht.readTemperature();
  if (isnan(sensorTemp))
  {
    Serial.println("Failed to read from DHT11 sensor!");
  }
  if (sensorTemp > 28){
    pose -= (sensorTemp - 28);
    myservo.write(pose);
  }
  else if (sensorTemp > 24){
    sensorLed = 2;
    digitalWrite(Green, HIGH);  
    digitalWrite(Red, LOW);
    digitalWrite(Yellow, LOW);  
  }
  else if (sensorTemp > 14) {
    sensorLed = 1;
    pose += 10;
    myservo.write(pose); 
    digitalWrite(Yellow, HIGH);
    digitalWrite(Green, LOW);
    digitalWrite(Red, LOW);
  }
  else{
    sensorLed = 0;
    pose += 20;
    myservo.write(pose); 
    digitalWrite(Red, HIGH);
    digitalWrite(Yellow, LOW);
    digitalWrite(Green, LOW);
  }
}
void loop() {

}
