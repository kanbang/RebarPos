//-----------------------------------------------------------------------------
//----- PosGroup.h : Declaration of PosGroup
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\PosGroup.h"

using namespace System;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ
{
    namespace RebarPosWrapper 
    {
        [Autodesk::AutoCAD::Runtime::Wrapper("CPosGroup")]
        public ref class PosGroup :  public Autodesk::AutoCAD::DatabaseServices::DBObject
        {
        public:
            PosGroup();

        internal:
            PosGroup(System::IntPtr unmanagedPointer, bool autoDelete);
            inline CPosGroup* GetImpObj()
            {
                return static_cast<CPosGroup*>(UnmanagedObject.ToPointer());
            }
		public:
			enum class DrawingUnits
			{ 
				Millimeter = 0,
				Centimeter = 1,
				Decimeter = 2,
				Meter = 3,
			};

		public:
			property String^ Name        { String^ get(); void set(String^ value); }
			property bool Bending        { bool get(); void set(bool value); }
			property double MaxBarLength { double get(); void set(double value); }
			property int Precision       { int get(); void set(int value); }

			property PosGroup::DrawingUnits DrawingUnit { PosGroup::DrawingUnits get(); void set(PosGroup::DrawingUnits value); }
			property PosGroup::DrawingUnits DisplayUnit { PosGroup::DrawingUnits get(); void set(PosGroup::DrawingUnits value); }

			property String^ Formula              { String^ get(); void set(String^ value); }
			property String^ FormulaWithoutLength { String^ get(); void set(String^ value); }
			property String^ FormulaPosOnly       { String^ get(); void set(String^ value); }
			
			property String^ StandardDiameters    { String^ get(); void set(String^ value); }

			property Autodesk::AutoCAD::Colors::Color^ TextColor       { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ PosColor        { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ CircleColor     { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ MultiplierColor { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ GroupColor      { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ NoteColor       { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ CurrentGroupHighlightColor { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId HiddenLayerId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId TextStyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
			property Autodesk::AutoCAD::DatabaseServices::ObjectId NoteStyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

			property double NoteScale { double get(); void set(double value); }

		public:
			property static String^ TableName        { String^ get(); }
		};
	}
}