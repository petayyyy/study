#include <Servo.h>
Servo servo1_1; Servo servo1_2; Servo servo2_1; Servo servo2_2;
uint32_t timer1;
uint32_t timer2;
int pose_servo1_1 = 0; int pose_servo1_2 = 0; int pose_servo2_1 = 0; int pose_servo2_2 = 0;

void defolt(){
  pose_servo1_1 = 0; pose_servo1_2 = 0; pose_servo2_1 = 0; pose_servo2_2 = 0;
  servo1_1.write(pose_servo1_1);
  servo1_2.write(pose_servo1_2);
  servo2_1.write(pose_servo2_1);
  servo2_2.write(pose_servo2_2);
  delay(5000);
}

// 0 - start all
// 1 - start 1
// 2 - start 2
void start(int a){
  if (a == 1){
    pose_servo1_1 = 0; pose_servo1_2 = 60; 
    servo1_1.write(pose_servo1_1);
    servo1_2.write(pose_servo1_2);
    delay(5000);
  }
  else if (a == 2){
    pose_servo2_1 = 0; pose_servo2_2 = 60;
    servo2_1.write(pose_servo2_1);
    servo2_2.write(pose_servo2_2);
    delay(5000);
  }
  else(
    pose_servo1_1 = 0; pose_servo1_2 = 60; pose_servo2_1 = 0; pose_servo2_2 = 60;
    servo1_1.write(pose_servo1_1);
    servo1_2.write(pose_servo1_2);
    servo2_1.write(pose_servo2_1);
    servo2_2.write(pose_servo2_2); 
    delay(20000);
  }
}

// 0, 30, 1 - down
// 1, 10, 0 - 1 move
// 2, 10, 0 - 2 move
void servo1_move(int a, int b, int f){
  for(int i=0; i < b; i++){
    if (a == 1){
      pose_servo1_1 += i;
      servo1_1.write(pose_servo1_1);
    }
    else if (a == 2){
      pose_servo2_1 += i;
      servo2_1.write(pose_servo2_1);
    }
    else{
      pose_servo1_1 += i;
      servo1_1.write(pose_servo1_1);
      pose_servo2_1 += i;
      servo2_1.write(pose_servo2_1);
    }
    timer1 = millis();
    while(millis() - timer1 >= 100){
      continue;
    }
  }
  if (f == 1){
    for(int i=0; i < b; i++){
      if (a == 1){
        pose_servo1_1 -= i;
        servo1_1.write(pose_servo1_1);
      }
      else if (a == 2){
        pose_servo2_1 -= i;
        servo2_1.write(pose_servo2_1);
      }
      else{
        pose_servo1_1 -= i;
        servo1_1.write(pose_servo1_1);
        pose_servo2_1 -= i;
        servo2_1.write(pose_servo2_1);
      }
      while(millis() - timer1 >= 100){
        continue;
      }
    }
  }
}

// 0, 50, 1 - down
// 1, 10, 0 - 1 move
// 2, 10, 0 - 2 move
void servo2_up(int a, int b, int f){
  for(int i=0; i < b; i++){
    if (f == 1){
      if (a == 1){
        pose_servo1_2 -= i;
        servo1_1.write(pose_servo1_2);
      }
      else if (a == 2){
        pose_servo2_2 -= i;
        servo2_1.write(pose_servo2_2);
      }
      else{
        pose_servo1_2 -= i;
        servo1_1.write(pose_servo1_2);
        pose_servo2_2 -= i;
        servo2_1.write(pose_servo2_2);
      }
      timer2 = millis();
      while(millis() - timer1 >= 100){
        continue;
      }
    }
    else{
      if (a == 1){
        pose_servo1_2 += i;
        servo1_1.write(pose_servo1_2);
      }
      else if (a == 2){
        pose_servo2_2 += i;
        servo2_1.write(pose_servo2_2);
      }
      else{
        pose_servo1_2 += i;
        servo1_1.write(pose_servo1_2);
        pose_servo2_2 += i;
        servo2_1.write(pose_servo2_2);
      }
      timer2 = millis();
      while(millis() - timer1 >= 100){
        continue;
      }
    }
  }
}

void setup() {
  Serial.begin(9600);
  servo1_1.attach(8);
  servo1_2.attach(9);
  servo2_1.attach(10);
  servo2_2.attach(11);
 }

void loop() {
  if (Serial.available() > 0) {
    String n = Serial.readString();
    if (n[0] == '1'){ // Если введено "1", то произойдет выставление манипулятора в его нулевое положение.
       Serial.println("Move to defolt");
       defolt();
    }
    else if (n[0] == '2'){ // Если введено "2", то произойдет выставление обоих манипуляторов в положение захвата дрона.
       Serial.println("Move to start all");
       start(0);
    }
    else if (n[0] == '3'){ // Если введено "3", то произойдет выставление первого манипуляторов в положение захвата дрона.
       Serial.println("Move to start 1");
       start(1);
    }
    else if (n[0] == '4'){ // Если введено "4", то произойдет выставление второго манипуляторов в положение захвата дрона.
       Serial.println("Move to start 2");
       start(2);
    }
    else if (n[0] == '5'){ // Если введено "5", то произойдет захват первым манипулятором.
      Serial.println("Capting 1");
      servo2_move(1, 10, 0);
      servo1_move(1, 10, 0);
    }
    else if (n[0] == '6'){ // Если введено "6", то произойдет захват вторым манипулятором.
       Serial.println("Capting 2");
       servo2_move(2, 10, 0);
       servo1_move(2, 10, 0);
    }
    else if (n[0] == '7'){ // Если введено "7", то произойдет посадка дрона.
       Serial.println("Landing Drone");
       servo2_move(0, 50, 1);
       servo1_move(0, 30, 1);
    }
  }
}
