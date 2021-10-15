import cv2

import numpy as np

import time



st1 = cv2.getStructuringElement(cv2.MORPH_RECT, (21, 21), (10, 10))

st2 = cv2.getStructuringElement(cv2.MORPH_RECT, (11, 11), (5, 5)) 

dict_flag, i, detect_flag = [], -1, True

cap = cv2.VideoCapture(0)



Led = { "red": [255, 0, 0], "blue": [0, 0, 255], "green": [0, 255, 0], "yellow": [255, 255, 0], "black": [0, 0, 0], "white": [255, 255, 255] }

count_led = 58

Lenta = [0]*count_led

if True:

    imgg = cv2.imread("/home/clover/Desktop/flag.jpg")

    #imgg = cv2.imread("/home/clover/Desktop/Flag_1.png")

    cv2.imshow("Image", imgg)

    if detect_flag:

        dict_flag.append([[0,0,0], [0,0,0], [0,0,0], [0,0,0], [0,0,0], [0,0,0]])

        detect_flag = False

        i+=1

        time.sleep(1)



    img = cv2.cvtColor(imgg, cv2.COLOR_BGR2HSV)

    # Detect Red

    frame = cv2.inRange(img, (0, 130, 172), (216, 255, 239))

    cnt = cv2.findContours(frame, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)[1]

    try: 

        for c in cnt:

            moments = cv2.moments(c, 1)       

            sum_pixel = moments['m00']

            if sum_pixel > 100:

                dict_flag[i][0] = [sum_pixel, int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)]

                detect_flag = True

                cv2.drawContours(imgg, [c], 0, (255,255,255), 2)

                cv2.putText(imgg, '*', (int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))

    except:

        pass



    # Detect Blue

    frame = cv2.inRange(img, (77, 149, 68), (123, 255, 187))

    cnt = cv2.findContours(frame, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)[1]

    try: 

        for c in cnt:

            moments = cv2.moments(c, 1)       

            sum_pixel = moments['m00']

            if sum_pixel > 100:

                dict_flag[i][1] = [sum_pixel, int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)]

                detect_flag = True

                cv2.drawContours(imgg, [c], 0, (255,255,255), 2)

                cv2.putText(imgg, '*', (int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))

    except:

        pass

    

    # Detect Black

    frame = cv2.inRange(img, (0, 0, 0), (100, 100, 100))

    cnt = cv2.findContours(frame, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)[1]

    try: 

        for c in cnt:

            moments = cv2.moments(c, 1)       

            sum_pixel = moments['m00']

            if sum_pixel > 100:

                dict_flag[i][2] = [sum_pixel, int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)]

                detect_flag = True

                cv2.drawContours(imgg, [c], 0, (255,255,255), 2)

                cv2.putText(imgg, '*', (int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))

    except:

        pass



    # Detect Yellow

    frame = cv2.inRange(img, (7, 61, 194), (60, 255, 255))

    cnt = cv2.findContours(frame, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)[1]

    try: 

        for c in cnt:

            moments = cv2.moments(c, 1)       

            sum_pixel = moments['m00']

            if sum_pixel > 100:

                dict_flag[i][3] = [sum_pixel, int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)]

                detect_flag = True

                cv2.drawContours(imgg, [c], 0, (255,255,255), 2)

                cv2.putText(imgg, '*', (int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))

    except:

        pass



    # Detect Green

    frame = cv2.inRange(img, (25, 217, 89), (74, 255, 168))

    cnt = cv2.findContours(frame, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)[1]

    try: 

        for c in cnt:

            moments = cv2.moments(c, 1)       

            sum_pixel = moments['m00']

            if sum_pixel > 100:

                dict_flag[i][4] = [sum_pixel, int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)]

                detect_flag = True

                cv2.drawContours(imgg, [c], 0, (255,255,255), 2)

                cv2.putText(imgg, '*', (int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))

    except:

        pass

    

    # Detect White

    frame = cv2.inRange(img, (0, 0, 208), (177, 150, 255))

    cnt = cv2.findContours(frame, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)[1]

    try: 

        for c in cnt:

            moments = cv2.moments(c, 1)       

            sum_pixel = moments['m00']

            if sum_pixel > 100:

                dict_flag[i][5] = [sum_pixel, int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)]

                detect_flag = True

                cv2.drawContours(imgg, [c], 0, (255,255,255), 2)

                cv2.putText(imgg, '*', (int(moments['m10'] / sum_pixel), int(moments['m01'] / sum_pixel)), cv2.FONT_HERSHEY_COMPLEX, 0.5, (0, 0, 0))

    except:

        pass

    

    print(dict_flag)



    # Led Strip

    if detect_flag:

        S = sum([ dict_flag[i][j][0] for j in range (len(dict_flag[i]))])

        j, k = 0, 0

        print(dict_flag[i][0][0])

        for k in range(int(dict_flag[i][0][0]/S * count_led)):

            Lenta[k+j] = ["red", dict_flag[i][0][1], dict_flag[i][0][2]]

        j+=k

        k = 0

        print(j, k )

        for k in range(int(dict_flag[i][1][0]/S * count_led)):

            Lenta[k+j] = ["blue", dict_flag[i][1][1], dict_flag[i][1][2]]

        j+=k

        k = 0

        print(j, k)

        for k in range(int(dict_flag[i][2][0]/S * count_led)):

            Lenta[k+j] = ["black", dict_flag[i][2][1], dict_flag[i][2][2]]

        j+=k

        k = 0

        for k in range(int(dict_flag[i][3][0]/S * count_led)):

            Lenta[k+j] = ["yellow", dict_flag[i][3][1], dict_flag[i][3][2]]

        j+=k

        k = 0

        for k in range(int(dict_flag[i][4][0]/S * count_led)):

            Lenta[k+j] = ["green", dict_flag[i][4][1], dict_flag[i][4][2]]

        j+=k

        k = 0

        for k in range(int(dict_flag[i][5][0]/S * count_led)):

            Lenta[k+j] = ["white", dict_flag[i][5][1], dict_flag[i][5][2]]

        j+=k

        for k in range(j+1, count_led):

            Lenta[k] = ["black", imgg.shape[1], imgg.shape[0]]

        

        # Analize data

        if abs(Lenta[0][1] - Lenta[count_led//2][1]) > 50:

            # Sorted by x

            Lenta.sort(key=lambda Lenta: Lenta[1])    

        else:

            # Sorted by y

            Lenta.sort(key=lambda Lenta: Lenta[2]) 

        

        print(Lenta)

    cv2.imshow("Result", imgg)

    cv2.imwrite("/home/clover/Desktop/resize.png", imgg)

    #if cv2.waitKey(1) and 0xFF == ord('q'):

    #    break

