#ifndef CommandParser_h
#define CommandParser_h

#ifndef WProgram_h
#include "WProgram.h"
#endif

#ifndef OpCodes_h
#include "OpCodes.h"
#endif

class CommandParser { 
private:

public:
  CommandParser();

  void Parse(String& text);
};

#endif

