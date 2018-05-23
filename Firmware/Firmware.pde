#ifndef Settings_h
#include "Settings.h"
#endif

#ifndef OpCodes_h
#include "OpCodes.h"
#endif

#ifndef MotorController_h
#include "MotorController.h"
#endif

#ifndef CommandParser_h
#include "CommandParser.h"
#endif

MotorController controller;
CommandParser commandParser;

Motor x_Motor(35, 29, 'X');
Motor y_Motor(37, 23, 'Y');
Motor z_Motor(33, 25, 'Z');
Motor a_Motor(31, 39, 'A');

Motor* xMotor;
Motor* yMotor;
Motor* zMotor;
Motor* aMotor;

void setup() {
  Serial.begin(115200);
  delay(20);
  
  //Wait for uar
  while(!Serial) { ; }
  
  pinMode(X_MIN_SENSOR_PIN, INPUT);
  pinMode(X_MAX_SENSOR_PIN, INPUT);
  pinMode(Y_MIN_SENSOR_PIN, INPUT);
  pinMode(Y_MAX_SENSOR_PIN, INPUT);
  pinMode(Z_MIN_SENSOR_PIN, INPUT);
  pinMode(Z_MAX_SENSOR_PIN, INPUT);
  
  pinMode(JOG_UP_PIN, INPUT);
  pinMode(JOG_DOWN_PIN, INPUT);
  pinMode(JOG_LEFT_PIN, INPUT);
  pinMode(JOG_RIGHT_PIN, INPUT);
  
  pinMode(TRIGGER_BUTTON_PIN, INPUT);  
  
  controller.Initialize();
  
  // X Pulley: 20 Tooth 2mm pitch
  // Pitch Ratio: 12.7254
  // Distance Covered in One Rev: Pitch Ratio * Math.Pi
  // Distance "" : 39,978023153991554876732522109585mm
  // 

  x_Motor.Initialize();
  x_Motor.IsInverted = true;
  x_Motor.SetDwellSpeed(100);
  x_Motor.ShortDistance = 6000;
  x_Motor.RampStartsAt = 60;
  x_Motor.MinPin = X_MIN_SENSOR_PIN;
  x_Motor.MaxPin = X_MAX_SENSOR_PIN;
  x_Motor.MicroStepRatio = 16;

  y_Motor.Initialize();
  y_Motor.IsInverted = true;
  y_Motor.SetDwellSpeed(50);
  y_Motor.ShortDistance = 8000;
  y_Motor.RampStartsAt = 60;
  y_Motor.MinPin = Y_MIN_SENSOR_PIN;
  y_Motor.MaxPin = Y_MAX_SENSOR_PIN;
  y_Motor.MicroStepRatio = 16;
  
  z_Motor.IsInverted = false;
  z_Motor.ShortDistance = 5000;
  z_Motor.RampStartsAt = 5;
  z_Motor.MinPin = Z_MIN_SENSOR_PIN;
  z_Motor.MaxPin = Z_MAX_SENSOR_PIN;
  z_Motor.Initialize();
  z_Motor.MicroStepRatio = 16;
  z_Motor.SetDwellSpeed(50);

  a_Motor.Initialize();
  a_Motor.IsInverted = false;
  a_Motor.SetDwellSpeed(10);
  a_Motor.ShortDistance = 26000;
  a_Motor.RampStartsAt = a_Motor.MaxSpeed * 0.1;
  a_Motor.MicroStepRatio = 32;
  a_Motor.SetDwellSpeed(80);
  
  x_Motor.SetMaxSpeed(150);
  y_Motor.SetMaxSpeed(150);
  z_Motor.SetMaxSpeed(75);
  a_Motor.SetMaxSpeed(50);

  x_Motor.SetSpeed(150);
  y_Motor.SetSpeed(150);
  z_Motor.SetSpeed(75);
  a_Motor.SetSpeed(50);

  controller.Motors[0] = x_Motor;
  controller.Motors[1] = y_Motor;
  controller.Motors[2] = z_Motor;
  controller.Motors[3] = a_Motor;

  delay(300);

  while(1) {
    uint8_t triggerPinState = digitalRead(TRIGGER_BUTTON_PIN);
    
    if (triggerPinState == LOW) {
       controller.BackOffset(); 
       break;
    }
  }
  
  xMotor = &controller.Motors[0];
  yMotor = &controller.Motors[1];
  zMotor = &controller.Motors[2];
  aMotor = &controller.Motors[3];
  
  yMotor->SetDwellSpeed(100);
  xMotor->SetDwellSpeed(100);
  zMotor->SetDwellSpeed(100);
  
  xMotor->SetMaxSpeed(700);
  xMotor->SetSpeed(700);
  yMotor->SetMaxSpeed(600);
  yMotor->SetSpeed(600);
  zMotor->SetMaxSpeed(200);
  zMotor->SetSpeed(200);
  aMotor->SetMaxSpeed(200);
  aMotor->SetSpeed(200);
}

String incomingData = "";


bool isWorking = false;

void loop() {
  /*long xPos = random(500, 27000);
  long yPos = random(500, 37000);

  m_Controller.LinearMoveTo(xPos, yPos, &x_Motor, &y_Motor);
  m_Controller.Move(2500, &z_Motor);
  m_Controller.Move(-2500, &z_Motor);
  
  xPos = random(500, 27000);
  yPos = random(500, 37000);
  
  m_Controller.LinearMoveTo(xPos, yPos, &x_Motor, &y_Motor);
  m_Controller.Move(2500, &z_Motor);
  delay(800);
  m_Controller.Move(-2500, &z_Motor);*/
  
  if (isWorking == false) {
     uint8_t jogUpState = digitalRead(JOG_UP_PIN);
     uint8_t jogDownState = digitalRead(JOG_DOWN_PIN);
     uint8_t jogLeftState = digitalRead(JOG_LEFT_PIN);
     uint8_t jogRightState = digitalRead(JOG_RIGHT_PIN);
     
     if (jogDownState == LOW) {
        if (yMotor->GetCurrentPosition() >= 0) {
           yMotor->Jog(1); 
        }
     }
     else if (jogUpState == LOW) {
        if (yMotor->GetCurrentPosition() > 0) {
          yMotor->Jog(-1); 
        }
     }
     else if (jogLeftState == LOW) {
        if (xMotor->GetCurrentPosition() > 0) {
           xMotor->Jog(-1); 
        }
     }
     else if (jogRightState == LOW) {
        xMotor->Jog(1);
     }
  }
  
  if (Serial.available()) {
    char c = Serial.read();
    incomingData += c;
  }
  
  if (incomingData.endsWith("\n")) {
    incomingData.replace("\n", "");
    commandParser.Parse(incomingData);
    incomingData = "";
  }
}
