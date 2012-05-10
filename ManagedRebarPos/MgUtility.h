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
			Utility(const Utility%) { }
			void operator=(const Utility%) { }

		public:
			static double EvaluateFormula(System::String^ formula);
			static bool ValidateFormula(System::String^ formula);
		};
	}
}