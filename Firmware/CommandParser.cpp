#include "CommandParser.h"
#ifndef MotorController_h
  #include "MotorController.h"
#endif

extern MotorController controller;

CommandParser::CommandParser() {

}

void CommandParser::Parse(String& text) {
  String opCodeField = text.substring(0, 4); // Format: 0x02
  text.replace(opCodeField + ",", "");
  text.replace("\r", "");
  
  char buffer[16]; 
  opCodeField.toCharArray(buffer, 16);

  uint16_t opCode = (uint8_t)strtol(buffer, NULL, 0);
  
  Serial.print("OpCode: "); Serial.println(opCode);
  
  switch(opCode) {
    case OpCodes::SetJogPrecision: {
      char speedBuffer[4];
      text.toCharArray(speedBuffer, 4);
      
      uint16_t speed = atol(speedBuffer);
      
      controller.SetJogPrecision(speed);
      
      
      Serial.print("Speed: ");
      Serial.println(speed);
      
      break; 
    }
  }
}
