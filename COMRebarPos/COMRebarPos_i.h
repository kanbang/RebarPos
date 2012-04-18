

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Wed Apr 18 12:47:48 2012
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

#ifndef __IComRebarPos_FWD_DEFINED__
#define __IComRebarPos_FWD_DEFINED__
typedef interface IComRebarPos IComRebarPos;
#endif 	/* __IComRebarPos_FWD_DEFINED__ */


#ifndef __ComRebarPos_FWD_DEFINED__
#define __ComRebarPos_FWD_DEFINED__

#ifdef __cplusplus
typedef class ComRebarPos ComRebarPos;
#else
typedef struct ComRebarPos ComRebarPos;
#endif /* __cplusplus */

#endif 	/* __ComRebarPos_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __COMPOLYLib_LIBRARY_DEFINED__
#define __COMPOLYLib_LIBRARY_DEFINED__

/* library COMPOLYLib */
/* [helpfile][helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_COMPOLYLib;

#ifndef __IComRebarPos_INTERFACE_DEFINED__
#define __IComRebarPos_INTERFACE_DEFINED__

/* interface IComRebarPos */
/* [unique][helpcontext][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IComRebarPos;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("4EB91FA1-8DD3-4DF9-9277-E0BA3EA5361F")
    IComRebarPos : public IAcadEntity
    {
    public:
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_BasePoint( 
            /* [retval][out] */ VARIANT *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_BasePoint( 
            /* [in] */ VARIANT newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_NoteGrip( 
            /* [retval][out] */ VARIANT *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_NoteGrip( 
            /* [in] */ VARIANT newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Pos( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Pos( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Count( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Count( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Diameter( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Diameter( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Spacing( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Spacing( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Multiplier( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Multiplier( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ShowLength( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ShowLength( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Note( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Note( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_A( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_A( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_B( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_B( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_C( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_C( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_D( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_D( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_E( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_E( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_F( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_F( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Length( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Shape( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Shape( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Group( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Group( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ShowMarkerOnly( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpcontext][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ShowMarkerOnly( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IComRebarPosVtbl
    {
        BEGIN_INTERFACE
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ struct GUID *riid,
            /* [out][idldescattr] */ void **ppvObj,
            /* [retval][out] */ void *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *AddRef )( 
            IComRebarPos * This,
            /* [retval][out] */ unsigned long *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Release )( 
            IComRebarPos * This,
            /* [retval][out] */ unsigned long *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IComRebarPos * This,
            /* [out][idldescattr] */ unsigned UINT *pctinfo,
            /* [retval][out] */ void *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ unsigned UINT itinfo,
            /* [in][idldescattr] */ unsigned long lcid,
            /* [out][idldescattr] */ void **pptinfo,
            /* [retval][out] */ void *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ struct GUID *riid,
            /* [in][idldescattr] */ signed char **rgszNames,
            /* [in][idldescattr] */ unsigned UINT cNames,
            /* [in][idldescattr] */ unsigned long lcid,
            /* [out][idldescattr] */ signed long *rgdispid,
            /* [retval][out] */ void *retval);
        
        /* [id][restricted][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IComRebarPos * This,
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
            IComRebarPos * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_ObjectName )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetXData )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ BSTR AppName,
            /* [out][idldescattr] */ VARIANT *XDataType,
            /* [out][idldescattr] */ VARIANT *XDataValue,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *SetXData )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ VARIANT XDataType,
            /* [in][idldescattr] */ VARIANT XDataValue,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Delete )( 
            IComRebarPos * This,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_ObjectID )( 
            IComRebarPos * This,
            /* [retval][out] */ LONG_PTR *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Application )( 
            IComRebarPos * This,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Database )( 
            IComRebarPos * This,
            /* [retval][out] */ IAcadDatabase **retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_HasExtensionDictionary )( 
            IComRebarPos * This,
            /* [retval][out] */ BOOLEAN *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetExtensionDictionary )( 
            IComRebarPos * This,
            /* [retval][out] */ IAcadDictionary **retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_OwnerID )( 
            IComRebarPos * This,
            /* [retval][out] */ LONG_PTR *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Document )( 
            IComRebarPos * This,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][hidden][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Erase )( 
            IComRebarPos * This,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_ObjectID32 )( 
            IComRebarPos * This,
            /* [retval][out] */ signed long *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_OwnerID32 )( 
            IComRebarPos * This,
            /* [retval][out] */ signed long *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_TrueColor )( 
            IComRebarPos * This,
            /* [retval][out] */ IAcadAcCmColor **retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_TrueColor )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ IAcadAcCmColor *noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Layer )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Layer )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ BSTR noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Linetype )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Linetype )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ BSTR noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_LinetypeScale )( 
            IComRebarPos * This,
            /* [retval][out] */ ACAD_NOUNITS *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_LinetypeScale )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ ACAD_NOUNITS noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Visible )( 
            IComRebarPos * This,
            /* [retval][out] */ BOOLEAN *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Visible )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ BOOLEAN noname,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *ArrayPolar )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ signed INT NumberOfObjects,
            /* [in][idldescattr] */ double AngleToFill,
            /* [in][idldescattr] */ VARIANT CenterPoint,
            /* [retval][out] */ VARIANT *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *ArrayRectangular )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ signed INT NumberOfRows,
            /* [in][idldescattr] */ signed INT NumberOfColumns,
            /* [in][idldescattr] */ signed INT NumberOfLevels,
            /* [in][idldescattr] */ double DistBetweenRows,
            /* [in][idldescattr] */ double DistBetweenCols,
            /* [in][idldescattr] */ double DistBetweenLevels,
            /* [retval][out] */ VARIANT *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Highlight )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ BOOLEAN HighlightFlag,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Copy )( 
            IComRebarPos * This,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Move )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ VARIANT FromPoint,
            /* [in][idldescattr] */ VARIANT ToPoint,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Rotate )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ VARIANT BasePoint,
            /* [in][idldescattr] */ double RotationAngle,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Rotate3D )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ VARIANT Point1,
            /* [in][idldescattr] */ VARIANT Point2,
            /* [in][idldescattr] */ double RotationAngle,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Mirror )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ VARIANT Point1,
            /* [in][idldescattr] */ VARIANT Point2,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Mirror3D )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ VARIANT Point1,
            /* [in][idldescattr] */ VARIANT Point2,
            /* [in][idldescattr] */ VARIANT point3,
            /* [retval][out] */ IDispatch **retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *ScaleEntity )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ VARIANT BasePoint,
            /* [in][idldescattr] */ double ScaleFactor,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *TransformBy )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ VARIANT TransformationMatrix,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *Update )( 
            IComRebarPos * This,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *GetBoundingBox )( 
            IComRebarPos * This,
            /* [out][idldescattr] */ VARIANT *MinPoint,
            /* [out][idldescattr] */ VARIANT *MaxPoint,
            /* [retval][out] */ void *retval);
        
        /* [id][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *IntersectWith )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ IDispatch *IntersectObject,
            /* [in][idldescattr] */ enum AcExtendOption option,
            /* [retval][out] */ VARIANT *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_PlotStyleName )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_PlotStyleName )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ BSTR noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Lineweight )( 
            IComRebarPos * This,
            /* [retval][out] */ ACAD_LWEIGHT *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Lineweight )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ ACAD_LWEIGHT noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Hyperlinks )( 
            IComRebarPos * This,
            /* [retval][out] */ IAcadHyperlinks **retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_Material )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propput][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_Material )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ BSTR noname,
            /* [retval][out] */ void *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_EntityName )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *retval);
        
        /* [id][propget][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_EntityType )( 
            IComRebarPos * This,
            /* [retval][out] */ signed long *retval);
        
        /* [id][propget][hidden][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *get_color )( 
            IComRebarPos * This,
            /* [retval][out] */ ACAD_COLOR *retval);
        
        /* [id][propput][hidden][funcdescattr] */ HRESULT ( STDMETHODCALLTYPE *put_color )( 
            IComRebarPos * This,
            /* [in][idldescattr] */ ACAD_COLOR noname,
            /* [retval][out] */ void *retval);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_BasePoint )( 
            IComRebarPos * This,
            /* [retval][out] */ VARIANT *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_BasePoint )( 
            IComRebarPos * This,
            /* [in] */ VARIANT newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_NoteGrip )( 
            IComRebarPos * This,
            /* [retval][out] */ VARIANT *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_NoteGrip )( 
            IComRebarPos * This,
            /* [in] */ VARIANT newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Pos )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Pos )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Count )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Count )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Diameter )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Diameter )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Spacing )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Spacing )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Multiplier )( 
            IComRebarPos * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Multiplier )( 
            IComRebarPos * This,
            /* [in] */ long newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ShowLength )( 
            IComRebarPos * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ShowLength )( 
            IComRebarPos * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Note )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Note )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_A )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_A )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_B )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_B )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_C )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_C )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_D )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_D )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_E )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_E )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_F )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_F )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Length )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Shape )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Shape )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Group )( 
            IComRebarPos * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Group )( 
            IComRebarPos * This,
            /* [in] */ BSTR newVal);
        
        /* [helpcontext][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ShowMarkerOnly )( 
            IComRebarPos * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpcontext][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ShowMarkerOnly )( 
            IComRebarPos * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        END_INTERFACE
    } IComRebarPosVtbl;

    interface IComRebarPos
    {
        CONST_VTBL struct IComRebarPosVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IComRebarPos_QueryInterface(This,riid,ppvObj,retval)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObj,retval) ) 

#define IComRebarPos_AddRef(This,retval)	\
    ( (This)->lpVtbl -> AddRef(This,retval) ) 

#define IComRebarPos_Release(This,retval)	\
    ( (This)->lpVtbl -> Release(This,retval) ) 

#define IComRebarPos_GetTypeInfoCount(This,pctinfo,retval)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo,retval) ) 

#define IComRebarPos_GetTypeInfo(This,itinfo,lcid,pptinfo,retval)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,itinfo,lcid,pptinfo,retval) ) 

#define IComRebarPos_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgdispid,retval)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgdispid,retval) ) 

#define IComRebarPos_Invoke(This,dispidMember,riid,lcid,wFlags,pdispparams,pvarResult,pexcepinfo,puArgErr,retval)	\
    ( (This)->lpVtbl -> Invoke(This,dispidMember,riid,lcid,wFlags,pdispparams,pvarResult,pexcepinfo,puArgErr,retval) ) 

#define IComRebarPos_get_Handle(This,retval)	\
    ( (This)->lpVtbl -> get_Handle(This,retval) ) 

#define IComRebarPos_get_ObjectName(This,retval)	\
    ( (This)->lpVtbl -> get_ObjectName(This,retval) ) 

#define IComRebarPos_GetXData(This,AppName,XDataType,XDataValue,retval)	\
    ( (This)->lpVtbl -> GetXData(This,AppName,XDataType,XDataValue,retval) ) 

#define IComRebarPos_SetXData(This,XDataType,XDataValue,retval)	\
    ( (This)->lpVtbl -> SetXData(This,XDataType,XDataValue,retval) ) 

#define IComRebarPos_Delete(This,retval)	\
    ( (This)->lpVtbl -> Delete(This,retval) ) 

#define IComRebarPos_get_ObjectID(This,retval)	\
    ( (This)->lpVtbl -> get_ObjectID(This,retval) ) 

#define IComRebarPos_get_Application(This,retval)	\
    ( (This)->lpVtbl -> get_Application(This,retval) ) 

#define IComRebarPos_get_Database(This,retval)	\
    ( (This)->lpVtbl -> get_Database(This,retval) ) 

#define IComRebarPos_get_HasExtensionDictionary(This,retval)	\
    ( (This)->lpVtbl -> get_HasExtensionDictionary(This,retval) ) 

#define IComRebarPos_GetExtensionDictionary(This,retval)	\
    ( (This)->lpVtbl -> GetExtensionDictionary(This,retval) ) 

#define IComRebarPos_get_OwnerID(This,retval)	\
    ( (This)->lpVtbl -> get_OwnerID(This,retval) ) 

#define IComRebarPos_get_Document(This,retval)	\
    ( (This)->lpVtbl -> get_Document(This,retval) ) 

#define IComRebarPos_Erase(This,retval)	\
    ( (This)->lpVtbl -> Erase(This,retval) ) 

#define IComRebarPos_get_ObjectID32(This,retval)	\
    ( (This)->lpVtbl -> get_ObjectID32(This,retval) ) 

#define IComRebarPos_get_OwnerID32(This,retval)	\
    ( (This)->lpVtbl -> get_OwnerID32(This,retval) ) 

#define IComRebarPos_get_TrueColor(This,retval)	\
    ( (This)->lpVtbl -> get_TrueColor(This,retval) ) 

#define IComRebarPos_put_TrueColor(This,noname,retval)	\
    ( (This)->lpVtbl -> put_TrueColor(This,noname,retval) ) 

#define IComRebarPos_get_Layer(This,retval)	\
    ( (This)->lpVtbl -> get_Layer(This,retval) ) 

#define IComRebarPos_put_Layer(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Layer(This,noname,retval) ) 

#define IComRebarPos_get_Linetype(This,retval)	\
    ( (This)->lpVtbl -> get_Linetype(This,retval) ) 

#define IComRebarPos_put_Linetype(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Linetype(This,noname,retval) ) 

#define IComRebarPos_get_LinetypeScale(This,retval)	\
    ( (This)->lpVtbl -> get_LinetypeScale(This,retval) ) 

#define IComRebarPos_put_LinetypeScale(This,noname,retval)	\
    ( (This)->lpVtbl -> put_LinetypeScale(This,noname,retval) ) 

#define IComRebarPos_get_Visible(This,retval)	\
    ( (This)->lpVtbl -> get_Visible(This,retval) ) 

#define IComRebarPos_put_Visible(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Visible(This,noname,retval) ) 

#define IComRebarPos_ArrayPolar(This,NumberOfObjects,AngleToFill,CenterPoint,retval)	\
    ( (This)->lpVtbl -> ArrayPolar(This,NumberOfObjects,AngleToFill,CenterPoint,retval) ) 

#define IComRebarPos_ArrayRectangular(This,NumberOfRows,NumberOfColumns,NumberOfLevels,DistBetweenRows,DistBetweenCols,DistBetweenLevels,retval)	\
    ( (This)->lpVtbl -> ArrayRectangular(This,NumberOfRows,NumberOfColumns,NumberOfLevels,DistBetweenRows,DistBetweenCols,DistBetweenLevels,retval) ) 

#define IComRebarPos_Highlight(This,HighlightFlag,retval)	\
    ( (This)->lpVtbl -> Highlight(This,HighlightFlag,retval) ) 

#define IComRebarPos_Copy(This,retval)	\
    ( (This)->lpVtbl -> Copy(This,retval) ) 

#define IComRebarPos_Move(This,FromPoint,ToPoint,retval)	\
    ( (This)->lpVtbl -> Move(This,FromPoint,ToPoint,retval) ) 

#define IComRebarPos_Rotate(This,BasePoint,RotationAngle,retval)	\
    ( (This)->lpVtbl -> Rotate(This,BasePoint,RotationAngle,retval) ) 

#define IComRebarPos_Rotate3D(This,Point1,Point2,RotationAngle,retval)	\
    ( (This)->lpVtbl -> Rotate3D(This,Point1,Point2,RotationAngle,retval) ) 

#define IComRebarPos_Mirror(This,Point1,Point2,retval)	\
    ( (This)->lpVtbl -> Mirror(This,Point1,Point2,retval) ) 

#define IComRebarPos_Mirror3D(This,Point1,Point2,point3,retval)	\
    ( (This)->lpVtbl -> Mirror3D(This,Point1,Point2,point3,retval) ) 

#define IComRebarPos_ScaleEntity(This,BasePoint,ScaleFactor,retval)	\
    ( (This)->lpVtbl -> ScaleEntity(This,BasePoint,ScaleFactor,retval) ) 

#define IComRebarPos_TransformBy(This,TransformationMatrix,retval)	\
    ( (This)->lpVtbl -> TransformBy(This,TransformationMatrix,retval) ) 

#define IComRebarPos_Update(This,retval)	\
    ( (This)->lpVtbl -> Update(This,retval) ) 

#define IComRebarPos_GetBoundingBox(This,MinPoint,MaxPoint,retval)	\
    ( (This)->lpVtbl -> GetBoundingBox(This,MinPoint,MaxPoint,retval) ) 

#define IComRebarPos_IntersectWith(This,IntersectObject,option,retval)	\
    ( (This)->lpVtbl -> IntersectWith(This,IntersectObject,option,retval) ) 

#define IComRebarPos_get_PlotStyleName(This,retval)	\
    ( (This)->lpVtbl -> get_PlotStyleName(This,retval) ) 

#define IComRebarPos_put_PlotStyleName(This,noname,retval)	\
    ( (This)->lpVtbl -> put_PlotStyleName(This,noname,retval) ) 

#define IComRebarPos_get_Lineweight(This,retval)	\
    ( (This)->lpVtbl -> get_Lineweight(This,retval) ) 

#define IComRebarPos_put_Lineweight(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Lineweight(This,noname,retval) ) 

#define IComRebarPos_get_Hyperlinks(This,retval)	\
    ( (This)->lpVtbl -> get_Hyperlinks(This,retval) ) 

#define IComRebarPos_get_Material(This,retval)	\
    ( (This)->lpVtbl -> get_Material(This,retval) ) 

#define IComRebarPos_put_Material(This,noname,retval)	\
    ( (This)->lpVtbl -> put_Material(This,noname,retval) ) 

#define IComRebarPos_get_EntityName(This,retval)	\
    ( (This)->lpVtbl -> get_EntityName(This,retval) ) 

#define IComRebarPos_get_EntityType(This,retval)	\
    ( (This)->lpVtbl -> get_EntityType(This,retval) ) 

#define IComRebarPos_get_color(This,retval)	\
    ( (This)->lpVtbl -> get_color(This,retval) ) 

#define IComRebarPos_put_color(This,noname,retval)	\
    ( (This)->lpVtbl -> put_color(This,noname,retval) ) 


#define IComRebarPos_get_BasePoint(This,pVal)	\
    ( (This)->lpVtbl -> get_BasePoint(This,pVal) ) 

#define IComRebarPos_put_BasePoint(This,newVal)	\
    ( (This)->lpVtbl -> put_BasePoint(This,newVal) ) 

#define IComRebarPos_get_NoteGrip(This,pVal)	\
    ( (This)->lpVtbl -> get_NoteGrip(This,pVal) ) 

#define IComRebarPos_put_NoteGrip(This,newVal)	\
    ( (This)->lpVtbl -> put_NoteGrip(This,newVal) ) 

#define IComRebarPos_get_Pos(This,pVal)	\
    ( (This)->lpVtbl -> get_Pos(This,pVal) ) 

#define IComRebarPos_put_Pos(This,newVal)	\
    ( (This)->lpVtbl -> put_Pos(This,newVal) ) 

#define IComRebarPos_get_Count(This,pVal)	\
    ( (This)->lpVtbl -> get_Count(This,pVal) ) 

#define IComRebarPos_put_Count(This,newVal)	\
    ( (This)->lpVtbl -> put_Count(This,newVal) ) 

#define IComRebarPos_get_Diameter(This,pVal)	\
    ( (This)->lpVtbl -> get_Diameter(This,pVal) ) 

#define IComRebarPos_put_Diameter(This,newVal)	\
    ( (This)->lpVtbl -> put_Diameter(This,newVal) ) 

#define IComRebarPos_get_Spacing(This,pVal)	\
    ( (This)->lpVtbl -> get_Spacing(This,pVal) ) 

#define IComRebarPos_put_Spacing(This,newVal)	\
    ( (This)->lpVtbl -> put_Spacing(This,newVal) ) 

#define IComRebarPos_get_Multiplier(This,pVal)	\
    ( (This)->lpVtbl -> get_Multiplier(This,pVal) ) 

#define IComRebarPos_put_Multiplier(This,newVal)	\
    ( (This)->lpVtbl -> put_Multiplier(This,newVal) ) 

#define IComRebarPos_get_ShowLength(This,pVal)	\
    ( (This)->lpVtbl -> get_ShowLength(This,pVal) ) 

#define IComRebarPos_put_ShowLength(This,newVal)	\
    ( (This)->lpVtbl -> put_ShowLength(This,newVal) ) 

#define IComRebarPos_get_Note(This,pVal)	\
    ( (This)->lpVtbl -> get_Note(This,pVal) ) 

#define IComRebarPos_put_Note(This,newVal)	\
    ( (This)->lpVtbl -> put_Note(This,newVal) ) 

#define IComRebarPos_get_A(This,pVal)	\
    ( (This)->lpVtbl -> get_A(This,pVal) ) 

#define IComRebarPos_put_A(This,newVal)	\
    ( (This)->lpVtbl -> put_A(This,newVal) ) 

#define IComRebarPos_get_B(This,pVal)	\
    ( (This)->lpVtbl -> get_B(This,pVal) ) 

#define IComRebarPos_put_B(This,newVal)	\
    ( (This)->lpVtbl -> put_B(This,newVal) ) 

#define IComRebarPos_get_C(This,pVal)	\
    ( (This)->lpVtbl -> get_C(This,pVal) ) 

#define IComRebarPos_put_C(This,newVal)	\
    ( (This)->lpVtbl -> put_C(This,newVal) ) 

#define IComRebarPos_get_D(This,pVal)	\
    ( (This)->lpVtbl -> get_D(This,pVal) ) 

#define IComRebarPos_put_D(This,newVal)	\
    ( (This)->lpVtbl -> put_D(This,newVal) ) 

#define IComRebarPos_get_E(This,pVal)	\
    ( (This)->lpVtbl -> get_E(This,pVal) ) 

#define IComRebarPos_put_E(This,newVal)	\
    ( (This)->lpVtbl -> put_E(This,newVal) ) 

#define IComRebarPos_get_F(This,pVal)	\
    ( (This)->lpVtbl -> get_F(This,pVal) ) 

#define IComRebarPos_put_F(This,newVal)	\
    ( (This)->lpVtbl -> put_F(This,newVal) ) 

#define IComRebarPos_get_Length(This,pVal)	\
    ( (This)->lpVtbl -> get_Length(This,pVal) ) 

#define IComRebarPos_get_Shape(This,pVal)	\
    ( (This)->lpVtbl -> get_Shape(This,pVal) ) 

#define IComRebarPos_put_Shape(This,newVal)	\
    ( (This)->lpVtbl -> put_Shape(This,newVal) ) 

#define IComRebarPos_get_Group(This,pVal)	\
    ( (This)->lpVtbl -> get_Group(This,pVal) ) 

#define IComRebarPos_put_Group(This,newVal)	\
    ( (This)->lpVtbl -> put_Group(This,newVal) ) 

#define IComRebarPos_get_ShowMarkerOnly(This,pVal)	\
    ( (This)->lpVtbl -> get_ShowMarkerOnly(This,pVal) ) 

#define IComRebarPos_put_ShowMarkerOnly(This,newVal)	\
    ( (This)->lpVtbl -> put_ShowMarkerOnly(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IComRebarPos_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_ComRebarPos;

#ifdef __cplusplus

class DECLSPEC_UUID("6421F67D-6A0A-4CA4-85BA-F11A0852A656")
ComRebarPos;
#endif
#endif /* __COMPOLYLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


