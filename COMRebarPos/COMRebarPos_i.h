

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Tue Apr 17 11:57:36 2012
 */
/* Compiler settings for .\COMRebarPos.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __COMRebarPos_i_h__
#define __COMRebarPos_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IComPolygon_FWD_DEFINED__
#define __IComPolygon_FWD_DEFINED__
typedef interface IComPolygon IComPolygon;
#endif 	/* __IComPolygon_FWD_DEFINED__ */


#ifndef __ComPolygon_FWD_DEFINED__
#define __ComPolygon_FWD_DEFINED__

#ifdef __cplusplus
typedef class ComPolygon ComPolygon;
#else
typedef struct ComPolygon ComPolygon;
#endif /* __cplusplus */

#endif 	/* __ComPolygon_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __COMPOLYLib_LIBRARY_DEFINED__
#define __COMPOLYLib_LIBRARY_DEFINED__

/* library COMPOLYLib */
/* [helpfile][helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_COMPOLYLib;

#ifndef __IComPolygon_INTERFACE_DEFINED__
#define __IComPolygon_INTERFACE_DEFINED__

/* interface IComPolygon */
/* [unique][helpcontext][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IComPolygon;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("4EB91FA1-8DD3-4DF9-9277-E0BA3EA5361F")
    IComPolygon : public IAcadEntity
    {
    public:
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_TextString( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_TextString( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Center( 
            /* [retval][out] */ VARIANT *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Center( 
            /* [in] */ VARIANT newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_TextStyleName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_TextStyleName( 
            /* [in] */ BSTR newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IComPolygonVtbl
    {
        BEGIN_INTERFACE
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IComPolygon * This,
            /* [in][idldescattr] */ struct GUID *riid,
            /* [out][idldescattr] */ void **ppvObj,
            /* [retval][out] */ void *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *AddRef )( 
            IComPolygon * This,
            /* [retval][out] */ unsigned long *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Release )( 
            IComPolygon * This,
            /* [retval][out] */ unsigned long *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IComPolygon * This,
            /* [out][idldescattr] */ unsigned UINT *pctinfo,
            /* [retval][out] */ void *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IComPolygon * This,
            /* [in][idldescattr] */ unsigned UINT itinfo,
            /* [in][idldescattr] */ unsigned long lcid,
            /* [out][idldescattr] */ void **pptinfo,
            /* [retval][out] */ void *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IComPolygon * This,
            /* [in][idldescattr] */ struct GUID *riid,
            /* [in][idldescattr] */ signed char **rgszNames,
            /* [in][idldescattr] */ unsigned UINT cNames,
            /* [in][idldescattr] */ unsigned long lcid,
            /* [out][idldescattr] */ signed long *rgdispid,
            /* [retval][out] */ void *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IComPolygon * This,
            /* [in][idldescattr] */ signed long dispidMember,
            /* [in][idldescattr] */ struct GUID *riid,
            /* [in][idldescattr] */ unsigned long lcid,
            /* [in][idldescattr] */ unsigned short wFlags,
            /* [in][idldescattr] */ struct DISPPARAMS *pdispparams,
            /* [out][idldescattr] */ VARIANT *pvarResult,
            /* [out][idldescattr] */ struct EXCEPINFO *pexcepinfo,
            /* [out][idldescattr] */ unsigned UINT *puArgErr,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Handle )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_ObjectName )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetXData )( 
            IComPolygon * This,
            /* [in][idldescattr] */ BSTR AppName,
            /* [out][idldescattr] */ VARIANT *XDataType,
            /* [out][idldescattr] */ VARIANT *XDataValue,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *SetXData )( 
            IComPolygon * This,
            /* [in][idldescattr] */ VARIANT XDataType,
            /* [in][idldescattr] */ VARIANT XDataValue,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Delete )( 
            IComPolygon * This,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_ObjectID )( 
            IComPolygon * This,
            /* [retval][out] */ LONG_PTR *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Application )( 
            IComPolygon * This,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Database )( 
            IComPolygon * This,
            /* [retval][out] */ IAcadDatabase **retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_HasExtensionDictionary )( 
            IComPolygon * This,
            /* [retval][out] */ BOOLEAN *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetExtensionDictionary )( 
            IComPolygon * This,
            /* [retval][out] */ IAcadDictionary **retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_OwnerID )( 
            IComPolygon * This,
            /* [retval][out] */ LONG_PTR *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Document )( 
            IComPolygon * This,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][hidden][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Erase )( 
            IComPolygon * This,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_ObjectID32 )( 
            IComPolygon * This,
            /* [retval][out] */ signed long *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_OwnerID32 )( 
            IComPolygon * This,
            /* [retval][out] */ signed long *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_TrueColor )( 
            IComPolygon * This,
            /* [retval][out] */ IAcadAcCmColor **retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_TrueColor )( 
            IComPolygon * This,
            /* [in][idldescattr] */ IAcadAcCmColor *noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Layer )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Layer )( 
            IComPolygon * This,
            /* [in][idldescattr] */ BSTR noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Linetype )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Linetype )( 
            IComPolygon * This,
            /* [in][idldescattr] */ BSTR noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_LinetypeScale )( 
            IComPolygon * This,
            /* [retval][out] */ ACAD_NOUNITS *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_LinetypeScale )( 
            IComPolygon * This,
            /* [in][idldescattr] */ ACAD_NOUNITS noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Visible )( 
            IComPolygon * This,
            /* [retval][out] */ BOOLEAN *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Visible )( 
            IComPolygon * This,
            /* [in][idldescattr] */ BOOLEAN noname,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *ArrayPolar )( 
            IComPolygon * This,
            /* [in][idldescattr] */ signed INT NumberOfObjects,
            /* [in][idldescattr] */ double AngleToFill,
            /* [in][idldescattr] */ VARIANT CenterPoint,
            /* [retval][out] */ VARIANT *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *ArrayRectangular )( 
            IComPolygon * This,
            /* [in][idldescattr] */ signed INT NumberOfRows,
            /* [in][idldescattr] */ signed INT NumberOfColumns,
            /* [in][idldescattr] */ signed INT NumberOfLevels,
            /* [in][idldescattr] */ double DistBetweenRows,
            /* [in][idldescattr] */ double DistBetweenCols,
            /* [in][idldescattr] */ double DistBetweenLevels,
            /* [retval][out] */ VARIANT *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Highlight )( 
            IComPolygon * This,
            /* [in][idldescattr] */ BOOLEAN HighlightFlag,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Copy )( 
            IComPolygon * This,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Move )( 
            IComPolygon * This,
            /* [in][idldescattr] */ VARIANT FromPoint,
            /* [in][idldescattr] */ VARIANT ToPoint,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Rotate )( 
            IComPolygon * This,
            /* [in][idldescattr] */ VARIANT BasePoint,
            /* [in][idldescattr] */ double RotationAngle,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Rotate3D )( 
            IComPolygon * This,
            /* [in][idldescattr] */ VARIANT Point1,
            /* [in][idldescattr] */ VARIANT Point2,
            /* [in][idldescattr] */ double RotationAngle,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Mirror )( 
            IComPolygon * This,
            /* [in][idldescattr] */ VARIANT Point1,
            /* [in][idldescattr] */ VARIANT Point2,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Mirror3D )( 
            IComPolygon * This,
            /* [in][idldescattr] */ VARIANT Point1,
            /* [in][idldescattr] */ VARIANT Point2,
            /* [in][idldescattr] */ VARIANT point3,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *ScaleEntity )( 
            IComPolygon * This,
            /* [in][idldescattr] */ VARIANT BasePoint,
            /* [in][idldescattr] */ double ScaleFactor,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *TransformBy )( 
            IComPolygon * This,
            /* [in][idldescattr] */ VARIANT TransformationMatrix,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Update )( 
            IComPolygon * This,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetBoundingBox )( 
            IComPolygon * This,
            /* [out][idldescattr] */ VARIANT *MinPoint,
            /* [out][idldescattr] */ VARIANT *MaxPoint,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *IntersectWith )( 
            IComPolygon * This,
            /* [in][idldescattr] */ IDispatch *IntersectObject,
            /* [in][idldescattr] */ enum AcExtendOption option,
            /* [retval][out] */ VARIANT *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_PlotStyleName )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_PlotStyleName )( 
            IComPolygon * This,
            /* [in][idldescattr] */ BSTR noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Lineweight )( 
            IComPolygon * This,
            /* [retval][out] */ ACAD_LWEIGHT *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Lineweight )( 
            IComPolygon * This,
            /* [in][idldescattr] */ ACAD_LWEIGHT noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Hyperlinks )( 
            IComPolygon * This,
            /* [retval][out] */ IAcadHyperlinks **retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Material )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Material )( 
            IComPolygon * This,
            /* [in][idldescattr] */ BSTR noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_EntityName )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_EntityType )( 
            IComPolygon * This,
            /* [retval][out] */ signed long *retval);
        
        /* [id][propget][hidden][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_color )( 
            IComPolygon * This,
            /* [retval][out] */ ACAD_COLOR *retval);
        
        /* [id][propput][hidden][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_color )( 
            IComPolygon * This,
            /* [in][idldescattr] */ ACAD_COLOR noname,
            /* [retval][out] */ void *retval);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_TextString )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_TextString )( 
            IComPolygon * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Center )( 
            IComPolygon * This,
            /* [retval][out] */ VARIANT *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Center )( 
            IComPolygon * This,
            /* [in] */ VARIANT newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_TextStyleName )( 
            IComPolygon * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_TextStyleName )( 
            IComPolygon * This,
            /* [in] */ BSTR newVal);
        
        END_INTERFACE
    } IComPolygonVtbl;

    interface IComPolygon
    {
        CONST_VTBL struct IComPolygonVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IComPolygon_QueryInterface(This,riid,ppvObj,retval)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObj,retval) ) 

#define IComPolygon_AddRef(This,retval)	\
    ( (This)->lpVtbl -> AddRef(This,retval) ) 

#define IComPolygon_Release(This,retval)	\
    ( (This)->lpVtbl -> Release(This,retval) ) 

#define IComPolygon_GetTypeInfoCount(This,pctinfo,retval)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo,retval) ) 

#define IComPolygon_GetTypeInfo(This,itinfo,lcid,pptinfo,retval)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,itinfo,lcid,pptinfo,retval) ) 

#define IComPolygon_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgdispid,retval)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgdispid,retval) ) 

#define IComPolygon_Invoke(This,dispidMember,riid,lcid,wFlags,pdispparams,pvarResult,pexcepinfo,puArgErr,retval)	\
    ( (This)->lpVtbl -> Invoke(This,dispidMember,riid,lcid,wFlags,pdispparams,pvarResult,pexcepinfo,puArgErr,retval) ) 

#define IComPolygon_get_Handle(This,retval)	\
    ( (This)->lpVtbl -> get_Handle(This,retval) ) 

#define IComPolygon_get_ObjectName(This,retval)	\
    ( (This)->lpVtbl -> get_ObjectName(This,retval) ) 

#define IComPolygon_GetXData(This,AppName,XDataType,XDataValue,retval)	\
    ( (This)->lpVtbl -> GetXData(This,AppName,XDataType,XDataValue,retval) ) 

#define IComPolygon_SetXData(This,XDataType,XDataValue,retval)	\
    ( (This)->lpVtbl -> SetXData(This,XDataType,XDataValue,retval) ) 

#define IComPolygon_Delete(This,retval)	\
    ( (This)->lpVtbl -> Delete(This,retval) ) 

#define IComPolygon_get_ObjectID(This,retval)	\
    ( (This)->lpVtbl -> get_ObjectID(This,retval) ) 

#define IComPolygon_get_Application(This,retval)	\
    ( (This)->lpVtbl -> get_Application(This,retval) ) 

#define IComPolygon_get_Database(This,retval)	\
    ( (This)->lpVtbl -> get_Database(This,retval) ) 

#define IComPolygon_get_HasExtensionDictionary(This,retval)	\
    ( (This)->lpVtbl -> get_HasExtensionDictionary(This,retval) ) 

#define IComPolygon_GetExtensionDictionary(This,retval)	\
    ( (This)->lpVtbl -> GetExtensionDictionary(This,retval) ) 

#define IComPolygon_get_OwnerID(This,retval)	\
    ( (This)->lpVtbl -> get_OwnerID(This,retval) ) 

#define IComPolygon_get_Document(This,retval)	\
    ( (This)->lpVtbl -> get_Document(This,retval) ) 

#define IComPolygon_Erase(This,retval)	\
    ( (This)->lpVtbl -> Erase(This,retval) ) 

#define IComPolygon_get_ObjectID32(This,retval)	\
    ( (This)->lpVtbl -> get_ObjectID32(This,retval) ) 

#define IComPolygon_get_OwnerID32(This,retval)	\
    ( (This)->lpVtbl -> get_OwnerID32(This,retval) ) 

#define IComPolygon_get_TrueColor(This,retval)	\
    ( (This)->lpVtbl -> get_TrueColor(This,retval) ) 

#define IComPolygon_put_TrueColor(This,noname,retval)	\
    ( (This)->lpVtbl -> put_TrueColor(This,noname,retval) ) 

#define IComPolygon_get_Layer(This,retval)	\
    ( (This)->lpVtbl -> get_Layer(This,retval) ) 

#define IComPolygon_put_Layer(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Layer(This,noname,retval) ) 

#define IComPolygon_get_Linetype(This,retval)	\
    ( (This)->lpVtbl -> get_Linetype(This,retval) ) 

#define IComPolygon_put_Linetype(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Linetype(This,noname,retval) ) 

#define IComPolygon_get_LinetypeScale(This,retval)	\
    ( (This)->lpVtbl -> get_LinetypeScale(This,retval) ) 

#define IComPolygon_put_LinetypeScale(This,noname,retval)	\
    ( (This)->lpVtbl -> put_LinetypeScale(This,noname,retval) ) 

#define IComPolygon_get_Visible(This,retval)	\
    ( (This)->lpVtbl -> get_Visible(This,retval) ) 

#define IComPolygon_put_Visible(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Visible(This,noname,retval) ) 

#define IComPolygon_ArrayPolar(This,NumberOfObjects,AngleToFill,CenterPoint,retval)	\
    ( (This)->lpVtbl -> ArrayPolar(This,NumberOfObjects,AngleToFill,CenterPoint,retval) ) 

#define IComPolygon_ArrayRectangular(This,NumberOfRows,NumberOfColumns,NumberOfLevels,DistBetweenRows,DistBetweenCols,DistBetweenLevels,retval)	\
    ( (This)->lpVtbl -> ArrayRectangular(This,NumberOfRows,NumberOfColumns,NumberOfLevels,DistBetweenRows,DistBetweenCols,DistBetweenLevels,retval) ) 

#define IComPolygon_Highlight(This,HighlightFlag,retval)	\
    ( (This)->lpVtbl -> Highlight(This,HighlightFlag,retval) ) 

#define IComPolygon_Copy(This,retval)	\
    ( (This)->lpVtbl -> Copy(This,retval) ) 

#define IComPolygon_Move(This,FromPoint,ToPoint,retval)	\
    ( (This)->lpVtbl -> Move(This,FromPoint,ToPoint,retval) ) 

#define IComPolygon_Rotate(This,BasePoint,RotationAngle,retval)	\
    ( (This)->lpVtbl -> Rotate(This,BasePoint,RotationAngle,retval) ) 

#define IComPolygon_Rotate3D(This,Point1,Point2,RotationAngle,retval)	\
    ( (This)->lpVtbl -> Rotate3D(This,Point1,Point2,RotationAngle,retval) ) 

#define IComPolygon_Mirror(This,Point1,Point2,retval)	\
    ( (This)->lpVtbl -> Mirror(This,Point1,Point2,retval) ) 

#define IComPolygon_Mirror3D(This,Point1,Point2,point3,retval)	\
    ( (This)->lpVtbl -> Mirror3D(This,Point1,Point2,point3,retval) ) 

#define IComPolygon_ScaleEntity(This,BasePoint,ScaleFactor,retval)	\
    ( (This)->lpVtbl -> ScaleEntity(This,BasePoint,ScaleFactor,retval) ) 

#define IComPolygon_TransformBy(This,TransformationMatrix,retval)	\
    ( (This)->lpVtbl -> TransformBy(This,TransformationMatrix,retval) ) 

#define IComPolygon_Update(This,retval)	\
    ( (This)->lpVtbl -> Update(This,retval) ) 

#define IComPolygon_GetBoundingBox(This,MinPoint,MaxPoint,retval)	\
    ( (This)->lpVtbl -> GetBoundingBox(This,MinPoint,MaxPoint,retval) ) 

#define IComPolygon_IntersectWith(This,IntersectObject,option,retval)	\
    ( (This)->lpVtbl -> IntersectWith(This,IntersectObject,option,retval) ) 

#define IComPolygon_get_PlotStyleName(This,retval)	\
    ( (This)->lpVtbl -> get_PlotStyleName(This,retval) ) 

#define IComPolygon_put_PlotStyleName(This,noname,retval)	\
    ( (This)->lpVtbl -> put_PlotStyleName(This,noname,retval) ) 

#define IComPolygon_get_Lineweight(This,retval)	\
    ( (This)->lpVtbl -> get_Lineweight(This,retval) ) 

#define IComPolygon_put_Lineweight(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Lineweight(This,noname,retval) ) 

#define IComPolygon_get_Hyperlinks(This,retval)	\
    ( (This)->lpVtbl -> get_Hyperlinks(This,retval) ) 

#define IComPolygon_get_Material(This,retval)	\
    ( (This)->lpVtbl -> get_Material(This,retval) ) 

#define IComPolygon_put_Material(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Material(This,noname,retval) ) 

#define IComPolygon_get_EntityName(This,retval)	\
    ( (This)->lpVtbl -> get_EntityName(This,retval) ) 

#define IComPolygon_get_EntityType(This,retval)	\
    ( (This)->lpVtbl -> get_EntityType(This,retval) ) 

#define IComPolygon_get_color(This,retval)	\
    ( (This)->lpVtbl -> get_color(This,retval) ) 

#define IComPolygon_put_color(This,noname,retval)	\
    ( (This)->lpVtbl -> put_color(This,noname,retval) ) 


#define IComPolygon_get_TextString(This,pVal)	\
    ( (This)->lpVtbl -> get_TextString(This,pVal) ) 

#define IComPolygon_put_TextString(This,newVal)	\
    ( (This)->lpVtbl -> put_TextString(This,newVal) ) 

#define IComPolygon_get_Center(This,pVal)	\
    ( (This)->lpVtbl -> get_Center(This,pVal) ) 

#define IComPolygon_put_Center(This,newVal)	\
    ( (This)->lpVtbl -> put_Center(This,newVal) ) 

#define IComPolygon_get_TextStyleName(This,pVal)	\
    ( (This)->lpVtbl -> get_TextStyleName(This,pVal) ) 

#define IComPolygon_put_TextStyleName(This,newVal)	\
    ( (This)->lpVtbl -> put_TextStyleName(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IComPolygon_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_ComPolygon;

#ifdef __cplusplus

class DECLSPEC_UUID("6421F67D-6A0A-4CA4-85BA-F11A0852A656")
ComPolygon;
#endif
#endif /* __COMPOLYLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


