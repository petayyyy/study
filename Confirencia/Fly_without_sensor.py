import rospy
from clover import srv
from std_srvs.srv import Trigger
from pyzbar import pyzbar
from cv_bridge import CvBridge
from sensor_msgs.msg import Image

pi = pigpio.pi()
pi.set_mode(13, pigpio.OUTPUT)
M = 540
m = 100
go = True
rospy.init_node('flight')

get_telemetry = rospy.ServiceProxy('get_telemetry', srv.GetTelemetry)
navigate = rospy.ServiceProxy('navigate', srv.Navigate)
navigate_global = rospy.ServiceProxy('navigate_global', srv.NavigateGlobal)
set_position = rospy.ServiceProxy('set_position', srv.SetPosition)
set_velocity = rospy.ServiceProxy('set_velocity', srv.SetVelocity)
set_attitude = rospy.ServiceProxy('set_attitude', srv.SetAttitude)
set_rates = rospy.ServiceProxy('set_rates', srv.SetRates)
land = rospy.ServiceProxy('land', Trigger)

bridge = CvBridge()

#rospy.init_node('barcode_test')


navigate(x=0, y=0, z=1, speed=1, frame_id='body', auto_arm=True)
rospy.sleep(4)

navigate(x=1, y=1, z=1, speed=1, frame_id='aruco_map')
rospy.sleep(5)

# Image subscriber callback function
def image_callback(data):
    global bridge, pi, M, m
    cv_image = bridge.imgmsg_to_cv2(data, 'bgr8')  # OpenCV image
    barcodes = pyzbar.decode(cv_image)
    for barcode in barcodes:
        b_data = barcode.data.encode("utf-8")
        number_bag = b_data[0]
        if number_bag == '509910002134':
            print ("QR detect bag delived:{}".format(number_bag))
            pi = pigpio.pi()
            pi.set_mode(13, pigpio.OUTPUT)
            M = 540
            telemetry = get_telemetry('body')
            h = (telemetry.z * M) / (M + m)
            print navigate(x=0, y=0, z=(telemetry.z - h), speed=1, frame_id='navigate_target')
            open()
            navigate(x=0, y=0, z=1, speed=1, frame_id='aruco_map')
            rospy.sleep(10)
            land()
            go = False

def open():
    pi.set_servo_pulsewidth(13, 1000)
    time.sleep(3)
    pi.set_servo_pulsewidth(13, 2000)
def ros_time():
    global go
    while go:
        rospy.rostime.wallsleep(0.01)

def start():
    image_sub = rospy.Subscriber('main_camera/image_raw', Image, image_callback, queue_size=1)
    ros_time()

start()
