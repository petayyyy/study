#include <ros.h>
#include <std_msgs/Float64.h>
#include "HX711.h"

HX711 scale(A1, A0); // DT, CLK

ros::NodeHandle  nh;

std_msgs::Float64 test;
ros::Publisher Sensor_Data("Data_Mass", &test);
float mass;
float Calibration_Factor_Of_Load_cell = -3.7;

void setup(){
  nh.initNode();
  nh.advertise(Sensor_Data);
  
  scale.set_scale();
  scale.tare();
}

void loop(){
  scale.set_scale(Calibration_Factor_Of_Load_cell);
  
  Serial.print("Sensor_Data: ");
  test.data = 0.035274 * scale.get_units();
  Sensor_Data.publish(&test);
  nh.spinOnce();
  Serial.print(0.035274 * scale.get_units(););
  Serial.println("//////////////////////////////");

  //delay(1000);  
  }
