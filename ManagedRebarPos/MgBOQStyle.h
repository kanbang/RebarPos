//-----------------------------------------------------------------------------
//----- BOQStyle.h : Declaration of BOQStyle
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\BOQStyle.h"

using namespace System;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ
{
    namespace RebarPosWrapper 
    {
        public ref class BOQStyle
        {
		protected:
			CBOQStyle* m_BOQStyle;

        internal:
            BOQStyle(CBOQStyle* style);
		public:
            BOQStyle();

		public:
			property String^ Name        { String^ get(); void set(String^ value); }

			property String^ Columns              { String^ get(); void set(String^ value); }

			property String^ PosLabel            { String^ get(); void set(String^ value); }
			property String^ CountLabel          { String^ get(); void set(String^ value); }
			property String^ DiameterLabel       { String^ get(); void set(String^ value); }
			property String^ LengthLabel         { String^ get(); void set(String^ value); }
			property String^ ShapeLabel          { String^ get(); void set(String^ value); }
			property String^ TotalLengthLabel    { String^ get(); void set(String^ value); }
			property String^ DiameterListLabel   { String^ get(); void set(String^ value); }
			property String^ DiameterLengthLabel { String^ get(); void set(String^ value); }
			property String^ UnitWeightLabel     { String^ get(); void set(String^ value); }
			property String^ WeightLabel         { String^ get(); void set(String^ value); }
			property String^ GrossWeightLabel    { String^ get(); void set(String^ value); }
			property String^ MultiplierHeadingLabel { String^ get(); void set(String^ value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId TextStyleId    { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
			property Autodesk::AutoCAD::DatabaseServices::ObjectId HeadingStyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
			property Autodesk::AutoCAD::DatabaseServices::ObjectId FootingStyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

			property bool IsBuiltIn         { bool get();    void set(bool value); }

		public:
			static void AddBOQStyle(BOQStyle^ shape);
			static BOQStyle^ GetBOQStyle(String^ name);
			static int GetBOQStyleCount();
			static System::Collections::Generic::Dictionary<String^, BOQStyle^>^ GetAllBOQStyles();
			static void ClearBOQStyles();
			static void ReadBOQStylesFromFile(String^ source);
			static void SaveBOQStylesToFile(String^ source);
		};
	}
}