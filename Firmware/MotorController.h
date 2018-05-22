#ifndef MotorController_h
#define MotorController_h

#ifndef Motor_h
  #include "Motor.h"
#endif;

#ifndef WProgram_h
  #include "WProgram.h"
#endif

#define MOTORS_COUNT 4

const char Axes[] = { 'X', 'Y', 'Z', 'A' };

class MotorController {
  public:
    MotorController();
    Motor Motors[5];
    bool UseRamp;
    void Initialize();
    void Delete(Motor motor);
    void Move(long steps, Motor *motor);
    void MoveTo(long pos, Motor *motor);
    void LinearMove(long steps1, long steps2, Motor *first, Motor *second);
    void LinearMoveTo(long steps1, long steps2, Motor *first, Motor *second);
    void BackOffset();
    void ForwardOffset();
    void CalculateRamp(unsigned long index, Motor * motor);
    void CalculateRamp(unsigned long delta, unsigned long index, Motor * motor);
    void SetJogPrecision(uint16_t speed);
    
    void JogMove(char axis, long steps);
    void Halt();

};

#endif
