#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
178,
183,
184,
185,
186,
187,
188,
189,
190,
193,
194,
246,
247,
249,
272,
273,
274,
285,
286,
287,
288,
361,
362,
363,
366,
396,
397,
399,
401,
403,
405,
410,
418,
419,
420,
421,
422,
423,
424,
425,
426,
427,
532,
540,
543,
545,
550,
551,
553,
554,
558,
559,
561,
562,
565,
566,
567,
570,
573,
575,
577,
638,
640,
642,
651,
652,
653,
655,
661,
662,
663,
664,
665,
673,
674,
675,
679,
680,
682,
684,
869,
1010,
1011,
5616,
5617,
5619,
5620,
5621,
5622,
5623,
5625,
5627,
5629,
5630,
5631,
5637,
5639,
5643,
5644,
5646,
5648,
5650,
5661,
5670,
5671,
5673,
5674,
5675,
5676,
5677,
5679,
5681,
6580,
6584,
6586,
6587,
6588,
6589,
6694,
6695,
6696,
6697,
6715,
6716,
6717,
6719,
6761,
6811,
6822,
6823,
6824,
7068,
7070,
7071,
7097,
7115,
7121,
7128,
7138,
7141,
7215,
7225,
7227,
7228,
7234,
7247,
7267,
7268,
7276,
7278,
7285,
7286,
7289,
7291,
7296,
7302,
7303,
7310,
7312,
7324,
7327,
7328,
7329,
7340,
7349,
7355,
7356,
7357,
7359,
7360,
7378,
7380,
7394,
7417,
7418,
7438,
7462,
7463,
7825,
7826,
7957,
8132,
8133,
8136,
8139,
8189,
8451,
8452,
9202,
9223,
9230,
9232,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementType_raw (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy_raw (int,int,int,int,int,int);
int ves_icall_System_Array_GetLength_raw (int,int,int);
int ves_icall_System_Array_GetLowerBound_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
int ves_icall_System_Array_GetValueImpl_raw (int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
int ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
void ves_icall_System_Enum_InternalBoxEnum_raw (int,int,int64_t,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_ModF (double,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Read_Long (int);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_test_synchronised_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
int ves_icall_System_Threading_Thread_GetCurrentProcessorNumber_raw (int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int mono_object_hash_icall_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw (int,int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
void ves_icall_AssemblyExtensions_ApplyUpdate (int,int,int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 178,
ves_icall_System_Array_InternalCreate,
// token 183,
ves_icall_System_Array_GetCorElementTypeOfElementType_raw,
// token 184,
ves_icall_System_Array_CanChangePrimitive,
// token 185,
ves_icall_System_Array_FastCopy_raw,
// token 186,
ves_icall_System_Array_GetLength_raw,
// token 187,
ves_icall_System_Array_GetLowerBound_raw,
// token 188,
ves_icall_System_Array_GetGenericValue_icall,
// token 189,
ves_icall_System_Array_GetValueImpl_raw,
// token 190,
ves_icall_System_Array_SetGenericValue_icall,
// token 193,
ves_icall_System_Array_SetValueImpl_raw,
// token 194,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 246,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 247,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 249,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 272,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 273,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 274,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 285,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 286,
ves_icall_System_Enum_InternalBoxEnum_raw,
// token 287,
ves_icall_System_Enum_InternalGetCorElementType,
// token 288,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 361,
ves_icall_System_Environment_get_ProcessorCount,
// token 362,
ves_icall_System_Environment_get_TickCount,
// token 363,
ves_icall_System_Environment_get_TickCount64,
// token 366,
ves_icall_System_Environment_FailFast_raw,
// token 396,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 397,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 399,
ves_icall_System_GC_SuppressFinalize_raw,
// token 401,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 403,
ves_icall_System_GC_GetGCMemoryInfo,
// token 405,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 410,
ves_icall_System_Object_MemberwiseClone_raw,
// token 418,
ves_icall_System_Math_Ceiling,
// token 419,
ves_icall_System_Math_Cos,
// token 420,
ves_icall_System_Math_Floor,
// token 421,
ves_icall_System_Math_Log,
// token 422,
ves_icall_System_Math_Log10,
// token 423,
ves_icall_System_Math_Pow,
// token 424,
ves_icall_System_Math_Sin,
// token 425,
ves_icall_System_Math_Sqrt,
// token 426,
ves_icall_System_Math_Tan,
// token 427,
ves_icall_System_Math_ModF,
// token 532,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 540,
ves_icall_RuntimeType_make_array_type_raw,
// token 543,
ves_icall_RuntimeType_make_byref_type_raw,
// token 545,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 550,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 551,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 553,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 554,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 558,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 559,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 561,
ves_icall_System_RuntimeType_getFullName_raw,
// token 562,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 565,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 566,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 567,
ves_icall_RuntimeType_GetFields_native_raw,
// token 570,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 573,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 575,
ves_icall_RuntimeType_GetName_raw,
// token 577,
ves_icall_RuntimeType_GetNamespace_raw,
// token 638,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 640,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 642,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 651,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 652,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 653,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 655,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 661,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 662,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 663,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 664,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 665,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 673,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 674,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 675,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 679,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 680,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 682,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 684,
ves_icall_System_String_FastAllocateString_raw,
// token 869,
ves_icall_System_Type_internal_from_handle_raw,
// token 1010,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1011,
ves_icall_System_ValueType_Equals_raw,
// token 5616,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 5617,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 5619,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 5620,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 5621,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 5622,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 5623,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 5625,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 5627,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 5629,
ves_icall_System_Threading_Interlocked_Read_Long,
// token 5630,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 5631,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 5637,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 5639,
mono_monitor_exit_icall_raw,
// token 5643,
ves_icall_System_Threading_Monitor_Monitor_test_synchronised_raw,
// token 5644,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 5646,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 5648,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 5650,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 5661,
ves_icall_System_Threading_Thread_GetCurrentProcessorNumber_raw,
// token 5670,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 5671,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 5673,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 5674,
ves_icall_System_Threading_Thread_GetState_raw,
// token 5675,
ves_icall_System_Threading_Thread_SetState_raw,
// token 5676,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 5677,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 5679,
ves_icall_System_Threading_Thread_YieldInternal,
// token 5681,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 6580,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 6584,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 6586,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 6587,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 6588,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 6589,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 6694,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 6695,
ves_icall_System_GCHandle_InternalFree_raw,
// token 6696,
ves_icall_System_GCHandle_InternalGet_raw,
// token 6697,
ves_icall_System_GCHandle_InternalSet_raw,
// token 6715,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 6716,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 6717,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 6719,
ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw,
// token 6761,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 6811,
mono_object_hash_icall_raw,
// token 6822,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 6823,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 6824,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 7068,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 7070,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 7071,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 7097,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 7115,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 7121,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 7128,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 7138,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 7141,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 7215,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 7225,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 7227,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 7228,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 7234,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 7247,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 7267,
ves_icall_reflection_get_token_raw,
// token 7268,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 7276,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 7278,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 7285,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 7286,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 7289,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 7291,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 7296,
ves_icall_reflection_get_token_raw,
// token 7302,
ves_icall_get_method_info_raw,
// token 7303,
ves_icall_get_method_attributes,
// token 7310,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 7312,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 7324,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 7327,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 7328,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 7329,
ves_icall_reflection_get_token_raw,
// token 7340,
ves_icall_InternalInvoke_raw,
// token 7349,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 7355,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 7356,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 7357,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 7359,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 7360,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 7378,
ves_icall_InvokeClassConstructor_raw,
// token 7380,
ves_icall_InternalInvoke_raw,
// token 7394,
ves_icall_reflection_get_token_raw,
// token 7417,
ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw,
// token 7418,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 7438,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 7462,
ves_icall_reflection_get_token_raw,
// token 7463,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 7825,
ves_icall_AssemblyExtensions_ApplyUpdate,
// token 7826,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 7957,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 8132,
ves_icall_ModuleBuilder_basic_init_raw,
// token 8133,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 8136,
ves_icall_ModuleBuilder_getToken_raw,
// token 8139,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 8189,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 8451,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 8452,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 9202,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 9223,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 9230,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 9232,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_handles [] = {
0,
1,
0,
1,
1,
1,
0,
1,
0,
1,
1,
0,
0,
0,
1,
1,
1,
1,
1,
0,
1,
0,
0,
0,
1,
1,
1,
1,
1,
0,
1,
1,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
0,
1,
1,
1,
1,
1,
1,
0,
1,
1,
0,
0,
1,
1,
1,
1,
1,
1,
1,
1,
0,
1,
1,
1,
1,
1,
1,
1,
1,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
1,
1,
1,
1,
1,
1,
1,
1,
1,
0,
1,
1,
1,
1,
1,
0,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
0,
0,
1,
1,
1,
1,
1,
1,
0,
1,
1,
1,
0,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
0,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
1,
0,
1,
1,
1,
1,
1,
1,
1,
1,
1,
0,
0,
0,
0,
};
