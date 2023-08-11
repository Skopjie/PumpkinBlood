from threading import Thread
import cv2
import platform
    
class Webcam:
    camera_is_ready = False
    head_direction_control = ""

    def __init__(self):
        self.stopped = False
        self.stream = None
        self.lastFrame = None
        self.os_name = platform.system()

    def start(self, head_control):
        t = Thread(target=self.update, args=())
        t.daemon = True
        t.start()
        self.head_direction_control = head_control
        return self

    def update(self):
        if self.stream is None:
            if self.os_name == "Windows":
                self.stream = cv2.VideoCapture(0, cv2.CAP_DSHOW)
            elif self.os_name == "Darwin": #macOS
                self.stream = cv2.VideoCapture(0, cv2.CAP_AVFOUNDATION)
            else: # Linux
                self.stream = cv2.VideoCapture(0, cv2.CAP_V4L)
        while True:
            if self.stopped:
                return
            (result, image) = self.stream.read()
            if not result:
                self.stop()
                return
            self.lastFrame = image
                
    def read(self):
        return self.lastFrame

    def stop(self):
        self.stopped = True

    def width(self):
        return self.stream.get(cv2.CAP_PROP_FRAME_WIDTH )

    def height(self):
        return self.stream.get(cv2.CAP_PROP_FRAME_HEIGHT )
    
    def ready(self):
        if self.lastFrame is not None:
            if self.camera_is_ready == False:
                self.camera_is_ready = True
                self.head_direction_control.send_message_unity()

        return self.lastFrame is not None