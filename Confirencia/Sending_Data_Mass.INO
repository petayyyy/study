#include <ros.h>
#include <std_msgs/Float64.h>

ros::NodeHandle  nh;

std_msgs::Float64 test;
ros::Publisher Sensor_Data("Data_Mass", &test);
float mass;

void setup(){
  nh.initNode();
  nh.advertise(Sensor_Data);
}

void loop(){
  
  Serial.print("Sensor_Data: ");
  test.data = sensor4.read();
  Sensor_Data.publish(&test);
  nh.spinOnce();
  Serial.print(sensor4.read());
  if (sensor4.timeoutOccurred()) { Serial.print(" TIMEOUT"); }
  Serial.println();

  //delay(1000);  
  }
