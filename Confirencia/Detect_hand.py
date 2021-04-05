import cv2
import numpy as np
import copy
import math
import os
def calculateFingers(res, drawing):
    #  convexity defect
    hull = cv2.convexHull(res, returnPoints=False)
    #cv2.drawContours(drawing, [hull], 0, (0, 0, 255), 3)
    if len(hull) > 3:
        defects = cv2.convexityDefects(res, hull)
        if defects is not None:
            cnt = 0
            for i in range(defects.shape[0]):  # calculate the angle
                s, e, f, d = defects[i][0]
                start = tuple(res[s][0])
                end = tuple(res[e][0])
                far = tuple(res[f][0]) 
                a = math.sqrt((end[0] - start[0]) ** 2 + (end[1] - start[1]) ** 2)
                b = math.sqrt((far[0] - start[0]) ** 2 + (far[1] - start[1]) ** 2)
                c = math.sqrt((end[0] - far[0]) ** 2 + (end[1] - far[1]) ** 2)
                angle = math.acos((b ** 2 + c ** 2 - a ** 2) / (2 * b * c))  # cosine theorem
                if angle <= math.pi / 2:  # angle less than 90 degree, treat as fingers
                    cnt += 1
                    cv2.circle(drawing, far, 8, [211, 84, 0], -1)
            if cnt > 0:
                if cnt > 4: cnt = 4
                return True, cnt+1
            else:
                return True, 0
    return False, 0

# Open Camera
camera = cv2.VideoCapture(0)
camera.set(10, 200)
while camera.isOpened():
    try:
    #Main Camera
        ret, frame = camera.read()
        if not ret:
            break        
        #frame = cv2.imread("hand2.jpg") 
        frame = cv2.bilateralFilter(frame, 5, 50, 100)  # Smoothing
        frame = cv2.flip(frame, 1)  #Horizontal Flip
        cv2.imshow('original', frame)
           #Background Removal
        bgModel = cv2.createBackgroundSubtractorMOG2(0, 255)
        fgmask = bgModel.apply(frame)
        kernel = np.ones((3, 3), np.uint8)
        fgmask = cv2.erode(fgmask, kernel, iterations=1)
        img = cv2.bitwise_and(frame, frame, mask=fgmask)
        
        cv2.imshow('original', img)
        imgr = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY) 
        edged = cv2.Canny(imgr, 10, 100, 3)
        cv2.imshow('edged', edged)     
        kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (7, 7))
        closed = cv2.morphologyEx(edged, cv2.MORPH_CLOSE, kernel)        
        contours = cv2.findContours(closed.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)[0]    
        
        length = len(contours)
        maxArea = -1
        if length > 0:
            for i in range(length):
                temp = contours[i]
                area = cv2.contourArea(temp)
                if area > maxArea:
                    maxArea = area
                    ci = i
            res = contours[ci]
            hull = cv2.convexHull(res)
            drawing = np.zeros(img.shape, np.uint8)
                
            isFinishCal, cnt = calculateFingers(res, drawing)
            #print ("Fingers", cnt)
            print()
            if cnt == 5: 
                print("Hand Open, I can delived pakage")
                cv2.drawContours(drawing, [res], 0, (0, 255, 0), 2)
                cv2.drawContours(drawing, [hull], 0, (0, 0, 255), 3)                
            else: print("I don't see hand or hand don't open")
            print()        
            cv2.imshow('output', drawing)
            
    except:pass
    k = cv2.waitKey(5) & 0xFF
    if k == 27:  # press ESC to exit
        exit()
        break
