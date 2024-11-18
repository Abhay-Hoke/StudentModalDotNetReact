import { Option } from "../models/Option";
import { Family, Nationality, Student } from "../models/Student";
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface CounterState {
    roles: Option[],
    relationships: Option[]
    selectedRole: string,
    students: Student[],
    studentForm: Student,
    familyForm: Family,
    showStudentForm: boolean,
    showFamilyForm: boolean,
    selectedStudentId: number,
    nationalities: Nationality[],
    familyMembers: Family[]
}

const initialState: CounterState = {
    roles: [
        {
            label: 'Admin',
            value: 'ADMIN'
        },
        {
            label: 'Registrar',
            value: 'REGISTRAR'
        }
    ],
    relationships: [
        {
            label: 'Father',
            value: 'Father'
        },
        {
            label: 'Mother',
            value: 'Mother'
        },
        {
            label: 'Brother',
            value: 'Brother'
        },
        {
            label: 'Sister',
            value: 'Sister'
        }
    ],
    selectedRole: 'ADMIN',
    students: [
    ],
    studentForm: {
        id: 0,
        dateOfBirth: new Date(),
        firstName: '',
        lastName: '',
        nationalityId: 0
    },
    showStudentForm: false,
    familyForm:{
        id: 0,
        dateOfBirth: new Date(),
        firstName: '',
        lastName: '',
        studentId: 0,
        relationship: 'Father',
        nationalityId: 0

    },
    showFamilyForm: false,
    selectedStudentId: 0,
    nationalities: [],
    familyMembers: []
};

export const studentSlice = createSlice({
    name: 'student',
    initialState,
    reducers: {
      addNewStudent: (state) => {
        state.showStudentForm = true
        state.studentForm = initialState.studentForm
      },
      editStudentDetails: (state, {payload}: PayloadAction<Student>) => {
        state.showStudentForm = true;
        state.studentForm = payload;
      },
      closeStudentForm:(state) => {
        state.showStudentForm = false;
      },
      addFamily: (state, {payload}: PayloadAction<number>) => {
        state.showFamilyForm = true;
        state.selectedStudentId = payload;
        state.familyForm = initialState.familyForm;
      },
      closeFamilyForm:(state) => {
        state.showFamilyForm = false;
      },
      setStudents: (state, {payload}: PayloadAction<Student[]>) => {
        state.students = payload;
      },
      setSelectedRole: (state, action: PayloadAction<string>) => {
        state.selectedRole = action.payload;
      },//problem here due to any option in student interface
      setStudentFormFieldValue: (state, action: PayloadAction<{field: string, value: string | number}>) => {
          state.studentForm[action.payload.field] = action.payload.value;
      },
      setStudentFormDOB: (state, action: PayloadAction<Date>) => {
          state.studentForm.dateOfBirth = action.payload;
      },//problem here due to any option in student interface
      setFamilyFormFieldValue: (state, action: PayloadAction<{field: string, value: string | number}>) => {
        state.familyForm[action.payload.field] = action.payload.value;
      },
      setFamilyFormDOB: (state, action: PayloadAction<Date>) => {
          state.familyForm.dateOfBirth = action.payload;
      },
      setNationalities: (state, {payload}: PayloadAction<Nationality[]>) => {
        state.nationalities = payload
      },
      setStudentNationality: (state, {payload}: PayloadAction<Nationality>) => {
        state.studentForm.nationalityId = payload.id
      },
      setStudentFamilyMembers: (state, {payload}: PayloadAction<Family[]>) => {
        state.familyMembers = payload
      },
      editFamilyDetails: (state, {payload}: PayloadAction<Family>) => {
        state.familyForm = payload
        state.showFamilyForm = true
      }
    },
});

export const { addNewStudent, editStudentDetails, 
    closeStudentForm, addFamily,
    closeFamilyForm,setStudents, setSelectedRole, 
    setStudentFormFieldValue,setStudentFormDOB,
    setFamilyFormFieldValue,setFamilyFormDOB,
    setNationalities,
    setStudentNationality,
    setStudentFamilyMembers,
    editFamilyDetails
  } = studentSlice.actions;
  
  export default studentSlice.reducer;