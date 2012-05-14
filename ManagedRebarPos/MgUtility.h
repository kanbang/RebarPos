//-----------------------------------------------------------------------------
//----- Utility.h : Declaration of utility functions
//-----------------------------------------------------------------------------

#pragma once

namespace OZOZ
{
    namespace RebarPosWrapper 
    {
        public ref class Utility
        {
        private:
			Utility() { }
			Utility(Utility%) { }
			void operator=(Utility%) { }

		public:
			static double EvaluateFormula(System::String^ formula);
			static bool ValidateFormula(System::String^ formula);
		};
	}
}