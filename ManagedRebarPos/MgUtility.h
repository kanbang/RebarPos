//-----------------------------------------------------------------------------
//----- PosUtility.h : Declaration of utility functions
//-----------------------------------------------------------------------------

#pragma once

using namespace System;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ
{
    namespace RebarPosWrapper 
    {
        public ref class PosUtility
        {
        private:
			PosUtility() { }
			PosUtility(PosUtility%) { }
			void operator=(PosUtility%) { }

		public:
			static double EvaluateFormula(System::String^ formula);
			static bool ValidateFormula(System::String^ formula);

			static Autodesk::AutoCAD::DatabaseServices::ObjectId CreateTextStyle(System::String^ name, System::String^ filename, double scale);

		public:
			property static Autodesk::AutoCAD::DatabaseServices::ObjectId DefpointsLayer { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); }
		};
	}
}