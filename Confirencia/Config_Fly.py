import rospy
import pigpio
import time
from std_msgs.msg import Float64
from clover import srv
from std_srvs.srv import Trigger

navigate = rospy.ServiceProxy('navigate', srv.Navigate)

class Delivery():
    def __init__(self):
        self.pi = pigpio.pi()
        self.pi.set_mode(13, pigpio.OUTPUT)
        self.M = 540
        self.flag = False
        
        self.data_sub = rospy.Subscriber("Data_Mass",Float64,self.callback) 
    def open(self):
        self.pi.set_servo_pulsewidth(13, 1000)
        time.sleep(3)
        self.pi.set_servo_pulsewidth(13, 2000)
    def callback(self,data):
        telemetry = get_telemetry('body')
        h = (telemetry.z * self.M) / (self.M + data)
        if self.flag == True:
            self.horizon()
            self.open()
            self.flag = False
    def horizon(self):
        print navigate(x=0, y=2, z=(telemetry.z - h), speed=1, frame_id='navigate_target')
        
