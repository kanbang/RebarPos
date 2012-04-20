//-----------------------------------------------------------------------------
//----- PosStyle.h : Declaration of PosStyle
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\PosStyle.h"

using namespace System;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ
{
    namespace RebarPosWrapper 
    {
        [Autodesk::AutoCAD::Runtime::Wrapper("CPosStyle")]
        public ref class PosStyle :  public Autodesk::AutoCAD::DatabaseServices::DBObject
        {
        public:
            PosStyle();

        internal:
            PosStyle(System::IntPtr unmanagedPointer, bool autoDelete);
            inline CPosStyle* GetImpObj()
            {
                return static_cast<CPosStyle*>(UnmanagedObject.ToPointer());
            }

		public:
			property String^ Formula { String^ get(); void set(String^ value); }
			property String^ FormulaWithoutLength { String^ get(); void set(String^ value); }
			property String^ FormulaPosOnly { String^ get(); void set(String^ value); }
			
			property Autodesk::AutoCAD::Colors::Color^ TextColor       { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ PosColor        { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ CircleColor     { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ MultiplierColor { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ GroupColor      { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ CurrentGroupHighlightColor { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId TextStyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
			property Autodesk::AutoCAD::DatabaseServices::ObjectId NoteStyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

			property double NoteScale { double get(); void set(double value); }

		public:
			property static String^ TableName        { String^ get(); }
		};
	}
}