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
        [Autodesk::AutoCAD::Runtime::Wrapper("CBOQStyle")]
		public ref class BOQStyle :  public Autodesk::AutoCAD::Runtime::RXObject
        {
		public:
            BOQStyle();

        internal:
            BOQStyle(System::IntPtr unmanagedPointer, bool autoDelete);
            inline CBOQStyle* GetImpObj()
            {
                return static_cast<CBOQStyle*>(UnmanagedObject.ToPointer());
            }

		public:
			property String^ Name                { String^ get(); void set(String^ value); }

			property String^ Columns             { String^ get(); void set(String^ value); }

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

			property bool IsBuiltIn         { bool get(); }

		public:
			static void AddBOQStyle(BOQStyle^ shape);
			static BOQStyle^ GetBOQStyle(String^ name);
			static BOQStyle^ GetUnknownBOQStyle();
			static bool HasBOQStyle(String^ name);
			static int GetBOQStyleCount();
			static System::Collections::Generic::List<String^>^ GetAllBOQStyles();
			static void ClearBOQStyles();
			static void ReadBOQStylesFromFile(String^ source);
			static void SaveBOQStylesToFile(String^ source);
		};
	}
}