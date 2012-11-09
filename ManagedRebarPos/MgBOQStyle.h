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
        public ref class BOQStyle :  public Autodesk::AutoCAD::DatabaseServices::DBObject
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
			enum class DrawingUnits
			{ 
				Millimeter = 0,
				Centimeter = 1,
				Decimeter = 2,
				Meter = 3,
			};

		public:
			property String^ Name        { String^ get(); void set(String^ value); }

			property int Precision       { int get(); void set(int value); }
			property BOQStyle::DrawingUnits DisplayUnit { BOQStyle::DrawingUnits get(); void set(BOQStyle::DrawingUnits value); }

			property String^ Columns              { String^ get(); void set(String^ value); }

			property Autodesk::AutoCAD::Colors::Color^ TextColor       { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ PosColor        { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ LineColor       { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ SeparatorColor  { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ BorderColor     { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ HeadingColor    { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ FootingColor    { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }

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

			property double HeadingScale { double get(); void set(double value); }
			property double FootingScale { double get(); void set(double value); }
			property double RowSpacing   { double get(); void set(double value); }

		public:
			property static String^ TableName        { String^ get(); }
		};
	}
}