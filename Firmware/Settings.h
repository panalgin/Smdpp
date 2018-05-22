#ifndef Settings_h
  #define Settings_h
  
#ifndef WProgram_h
  #include "WProgram.h"
#endif

static char __attribute__((coherent)) RAMBUFF[64];

static const uint8_t X_MAX_SENSOR_PIN = 53;
static const uint8_t X_MIN_SENSOR_PIN = 53;

static const uint8_t Y_MAX_SENSOR_PIN = 49;
static const uint8_t Y_MIN_SENSOR_PIN = 49;

static const uint8_t Z_MAX_SENSOR_PIN = 51;
static const uint8_t Z_MIN_SENSOR_PIN = 51;

static const uint8_t JOG_LEFT_PIN = 52;
static const uint8_t JOG_RIGHT_PIN = 46;
static const uint8_t JOG_UP_PIN = 48;
static const uint8_t JOG_DOWN_PIN = A15;

static const uint8_t TRIGGER_BUTTON_PIN = A14;

#endif
