# -*- coding: utf-8 -*-
import numpy as np
import math
import cv2
import time
import roslib
import sys
import rospy
from sensor_msgs.msg import Image
import threading
from cv2 import aruco
from cv_bridge import CvBridge, CvBridgeError
from mavros_msgs.srv import CommandBool
from clover import srv
from std_srvs.srv import Trigger
from sensor_msgs.msg import Range

arming = rospy.ServiceProxy('mavros/cmd/arming', CommandBool)
get_telemetry = rospy.ServiceProxy('get_telemetry', srv.GetTelemetry)
navigate = rospy.ServiceProxy('navigate', srv.Navigate)
navigate_global = rospy.ServiceProxy('navigate_global', srv.NavigateGlobal)
set_position = rospy.ServiceProxy('set_position', srv.SetPosition)
set_velocity = rospy.ServiceProxy('set_velocity', srv.SetVelocity)
set_attitude = rospy.ServiceProxy('set_attitude', srv.SetAttitude)
set_rates = rospy.ServiceProxy('set_rates', srv.SetRates)
land = rospy.ServiceProxy('land', Trigger)

class ColorDetecting():                                                                                              
    def __init__(self):                                                                                              
        rospy.init_node('Color_detect', anonymous=True)                                                              
        self.image_pub = rospy.Publisher("Debug",Image,queue_size=10)                                               
        
        self.red_low = np.array([0, 70, 50])                                                                       
        self.red_high =  np.array([10, 255, 255])                                                                    
      
        self.yellow_low = np.array([21, 93, 86])                                                                    
        self.yellow_high = np.array([118, 255, 255])

        self.green_low = np.array([51,114,86])                                                                     
        self.green_high = np.array([142,255,255])
        
        self.Color = False       
        self.mas = []
        self.mas_last = []
        self.bridge = CvBridge()                                                                                     
        self.image_sub = rospy.Subscriber("main_camera/image_raw_throttled",Image,self.callback)  #!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    def point(self):
        numberLine = 0
        dist = 0.5
        massiv_new = sorted(self.mas, key=lambda k: [k[1], k[0]])
        Points = {numberLine: [massiv_new[0]]}
        x_last, y_last = massiv_new[0][0], massiv_new[0][1]
        for line in massiv_new:
            if max(x_last, line[0]) - min(x_last, line[0]) <= dist and max(y_last, line[1]) - min(y_last, line[1]) <= dist:
                Points[numberLine].append(line)
                
            else:
                numberLine += 1
                Points[numberLine] = [line]
            x_last, y_last = line[0], line[1]

        Point = {}

        for numberLine in Points:
            Point[numberLine] = [abs(round(sum(x)/len(x), 2)) for x in zip(*Points[numberLine])]
            if Point[numberLine][-1] == 0:
                Point[numberLine][-1] = 'Red'
            elif Point[numberLine][-1] == 2:
                Point[numberLine][-1] = 'Green'
            elif Point[numberLine][-1] == 1:
                Point[numberLine][-1] = 'Yelow'
            elif Point[numberLine][-1] == 3:
                Point[numberLine][-1] = 'Red_2'
            elif Point[numberLine][-1] == 5:
                Point[numberLine][-1] = 'Green_2'
            elif Point[numberLine][-1] == 4:
                Point[numberLine][-1] = 'Yelow_2'
            
        return Point
    def distance_x(self,x,z):
        if x >= 120:
            return (-(x - 120))* z / 82.2240375817873 + 0.05
        else:
            return (120 - x)* z / 82.2240375817873
    def distance_y(self,y,z):
        if y >= 160:
            return (y - 160)* z / 90.89757992875512
        else:
            return -(160 - y)* z / 90.89757992875512
    def distance_cam(self, rect):
        #print('koi')
        MARKER_SIDE1_SIZE = 0.5
        MARKER_SIDE2_SIZE = 0.5
        op = np.array([(-MARKER_SIDE1_SIZE / 2, -MARKER_SIDE2_SIZE / 2, 0), (MARKER_SIDE1_SIZE / 2, -MARKER_SIDE2_SIZE / 2, 0), (MARKER_SIDE1_SIZE / 2, MARKER_SIDE2_SIZE / 2, 0), (-MARKER_SIDE1_SIZE / 2, MARKER_SIDE2_SIZE / 2, 0)])
        cM = np.array([[92.37552065968626, 0.0, 160.5], [0.0, 92.37552065968626, 120.5], [0.0, 0.0, 1.0]])
        dC = np.zeros((8, 1), dtype="float64")
        rvecs = np.array([])
        tvecs = np.array([])
        #retval, rvec, tvec = cv2.solvePnP(np.array(op, dtype="float32"), np.array(rect, dtype="float32"), cM, dC)
        rvecs, tvecs, _objPoints	=	cv2.aruco.estimatePoseSingleMarkers(np.array(rect, dtype="float32"), 0.5, cM, dC, rvecs, tvecs)
        print('cx_cam=',tvec[1][0], 'cy_cam=',tvec[0][0], 'cz_cam=',tvec[2][0])
        return [tvec[0][0],tvec[1][0]]
    def callback(self,data):                                                                                         # Основная функция (data- изображения из типа msg)
        if self.Color == True:
            try:                                                                                                         # Считывание и конвертация изображения в вид пригодный для дальнейшей работы (try- для отсутствия ошибки если топик будет пустой)
                img = self.bridge.imgmsg_to_cv2(data, "bgr8")
            except:pass
            start = get_telemetry(frame_id='aruco_map')
            startz = rospy.wait_for_message('rangefinder/range', Range)
            Grey = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
             
            mask1 = cv2.inRange(Grey, self.red_low, self.red_high)                                                          # Создание облак точек для каждого цвета
            mask2 = cv2.inRange(Grey, self.yellow_low, self.yellow_high)
            mask3 = cv2.inRange(Grey, self.green_low, self.green_high)
            
            st1 = cv2.getStructuringElement(cv2.MORPH_RECT, (21, 21), (10, 10))
            st2 = cv2.getStructuringElement(cv2.MORPH_RECT, (11, 11), (5, 5))
            thresh = cv2.morphologyEx(mask1, cv2.MORPH_CLOSE, st1)
            thresh = cv2.morphologyEx(thresh, cv2.MORPH_OPEN, st2)
            
            _, red, hier = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)                     # Поиск контуров в облаке точек (Красном)
            for c in red:                                                                                                # Перебор каждого контура
                y,x = 0,0
                moments = cv2.moments(c, 1)                                                                   # Метод создающий матрицу объекта
                sum_y = moments['m01']
                sum_x = moments['m10']
                sum_pixel = moments['m00']
                if sum_pixel > 500:
                    y = int(sum_x / sum_pixel)
                    x = int(sum_y / sum_pixel)
                    x_d = self.distance_x(x,startz.range)
                    y_d = self.distance_y(y,startz.range)
                    approx = cv2.approxPolyDP(c, 0.04* cv2.arcLength(c, True), True)
                    points_img = np.array([np.array(p[0]) for p in approx])
                    y_d_2, x_d_2 = self.distance_cam(points_img)
                    if self.Color == True:
                        print('Red x_d,y_d',start.x+x_d,start.y-y_d)
                        self.mas_last.append([start.x+x_d,start.y-y_d,0])
                        print('Red x_d_2,y_d_2',start.x-x_d_2,start.y-y_d_2)
                        self.mas.append([start.x-x_d_2,start.y-y_d_2,0])
                    cv2.putText(img, 'Red', (y, x), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))                       
                    cv2.drawContours(img, [c], 0, (193,91,154), 2)
                	
            thresh = cv2.morphologyEx(mask2 - mask3, cv2.MORPH_CLOSE, st1)
            thresh = cv2.morphologyEx(thresh, cv2.MORPH_OPEN, st2)
            _, yellow, hier = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)                        # И желтого
            for c in yellow:
                try:
                    moments = cv2.moments(c, 1)                                                                   # Метод создающий матрицу объекта
                    sum_y = moments['m01']
                    sum_x = moments['m10']
                    sum_pixel = moments['m00']
                    if sum_pixel > 500:
                        y = int(sum_x / sum_pixel)
                        x = int(sum_y / sum_pixel)
                        x_d = self.distance_x(x,startz.range)
                        y_d = self.distance_y(y,startz.range)
                        approx = cv2.approxPolyDP(c, 0.01* cv2.arcLength(c, True), True)
                        points_img = np.array([np.array(p[0]) for p in approx])
                        y_d_2, x_d_2 = self.distance_cam(points_img)
                        if math.sqrt(x_d**2+y_d**2) <= 1 and self.Color == True:
                            print('Yellow x_d,y_d',start.x+x_d,start.y-y_d)
                            self.mas_last.append([start.x+x_d,start.y-y_d,1])
                            print('Yellow x_d_2,y_d_2',start.x-x_d_2,start.y-y_d_2)
                            self.mas.append([start.x-x_d_2,start.y-y_d_2,1])
                        cv2.putText(img, 'Yellow', (y, x), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))
                        cv2.drawContours(img, [c], 0, (193,91,154), 2)                         
                except:pass
            
            thresh = cv2.morphologyEx(mask3, cv2.MORPH_CLOSE, st1)
            thresh = cv2.morphologyEx(thresh, cv2.MORPH_OPEN, st2)
            _, green, hier = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)                        # И желтого
            for c in green:
                try:   
                    moments = cv2.moments(c, 1)                                                                   # Метод создающий матрицу объекта
                    sum_y = moments['m01']
                    sum_x = moments['m10']
                    sum_pixel = moments['m00']
                    if sum_pixel > 500:
                        y = int(sum_x / sum_pixel)
                        x = int(sum_y / sum_pixel)
                        x_d = self.distance_x(x,startz.range)
                        y_d = self.distance_y(y,startz.range)
                        approx = cv2.approxPolyDP(c, 0.01* cv2.arcLength(c, True), True)
                        points_img = np.array([np.array(p[0]) for p in approx])
                        y_d_2, x_d_2 = self.distance_cam(points_img)
                        if math.sqrt(x_d**2+y_d**2) <= 1 and self.Color == True:
                            print('Green x_d,y_d',start.x+x_d,start.y-y_d)
                            self.mas_last.append([start.x+x_d,start.y-y_d,2])
                            print('Green x_d_2,y_d_2',start.x-x_d_2,start.y-y_d_2)
                            self.mas.append([start.x-x_d_2,start.y-y_d_2,2])
                        cv2.putText(img, 'Green', (y, x), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))
                        cv2.drawContours(img, [c], 0, (193,91,154), 2)                    
                except:pass
            
            
            try:
                self.image_pub.publish(self.bridge.cv2_to_imgmsg(img, "bgr8"))                                           # Вывод конвертипованного изображения
            except CvBridgeError as e:
                print(e)
                    
def main():                                                                                                      # Начальная функция
  global col_det
  col_det = ColorDetecting()                                                                                         # Обращение к классу Color_detect

n = 4

print navigate(x=0, y=0, z=1, speed=0.5, frame_id='body', auto_arm=True)
rospy.sleep(3)

print navigate(x=0, y=0, z=1, speed=0.5, frame_id='aruco_map')
rospy.sleep(3)
main()
print('ready')

for i in range (n+1):
    for j in range (n*2+1):
        if i % 2 == 0:
            print navigate(x=j*0.5, y=i, z=1, speed=0.25, frame_id='aruco_map')
        else:
            print navigate(x=n-j*0.5, y=i, z=1, speed=0.25, frame_id='aruco_map')
        rospy.sleep(2)
        col_det.Color = True
        rospy.sleep(0.5)
col_det.Color = False


print navigate(x=0, y=0, z=1, speed=5, frame_id='aruco_map')
rospy.sleep(6)

land()
print(col_det.mas_last)

print('//////////////////////////////////')

print(col_det.mas)


f = open('Data11.txt', 'w')
f.write(' Cvet raspoznanogo markera       Coordinati\n')
for i in range (len(col_det.mas)):
    #print("{}.             x={}                    y={}".format(col_det.mas[i][2], col_det.mas[i][0], col_det.mas[i][1]))
    f.write("{}.             x={}                    y={}\n".format(col_det.mas[i][2], col_det.mas[i][0], col_det.mas[i][1]) )
f.close()

print('Podogdite obrabotky dannix') 
markers = col_det.point()

k = 1
f = open('Data9.txt', 'w')
print(markers)
print('Nomer tochki       Coordinati                    Cvet raspoznanogo markera')
f.write('Nomer tochki       Coordinati                    Cvet raspoznanogo markera\n')

for j in markers.values():
    print("{}.             x={}                    y={}			{}".format(k, j[0], j[1],j[2]))
    f.write("{}.             x={}                    y={}		{}\n".format(k, j[0], j[1],j[2]) )
    k+=1
f.close()
