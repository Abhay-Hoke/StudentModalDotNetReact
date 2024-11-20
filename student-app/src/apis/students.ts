import axios, { AxiosError, AxiosInstance, AxiosResponse } from 'axios'
import { Family, Nationality, Student } from '../models/Student.ts'
import { AppDispatch } from '../app/store.ts'
import { closeFamilyForm, closeStudentForm, setNationalities, setStudentFamilyMembers, setStudentNationality, setStudents } from '../features/StudentSlice.ts'

// Factory function to create a new Axios instance
export const createModuleAPI = (baseURL: string): AxiosInstance => {
  const instance = axios.create({ baseURL })
  return instance
}

export const StudentAPI = createModuleAPI(`https://localhost:7046/api/Students`)

export const AddNewStudent = async (student: Student, dispatch: AppDispatch) => {
    const updatedStudent = {
        ...student,
        nationalityId: null
    }
    if(student.id > 0) {
       const data = await StudentAPI.put(`/students/${student.id}`,updatedStudent);
       await UpdateStudentNationality(dispatch, student.id, student.nationalityId);
    } else {
        const data = await StudentAPI.post('/students',updatedStudent);
        //TODO - Please fix this in your code because 
       //await UpdateStudentNationality(dispatch, data.id, student.nationalityId);

    }
    await GetAllStudents(dispatch);
    dispatch(closeStudentForm());
}

export const GetStudentNationality = async (studentId: number): Promise<Student> => {
    const response = await StudentAPI.get<Student>(`/students/${studentId}/Nationality`);
    return response.data;
}


export const UpdateStudentNationality = async (dispatch: AppDispatch, studentId: number, nationalityId?: number) => {
    await StudentAPI.put(`/students/${studentId}/Nationality/${nationalityId}`);
    await GetAllStudents(dispatch);
    dispatch(closeStudentForm());
}

export const GetAllStudents = async (dispatch: AppDispatch) => {
    const response = await StudentAPI.get<Student[]>('/students');
    if(response) {
        dispatch(setStudents(response.data))
    }
}

export const AddNewFamilyMember = async (studentId: number, family: Family, dispatch: AppDispatch) => {
    const updatedFamily = {
        ...family,
        nationalityId: null,
        relationshipId: studentId
    }
    await StudentAPI.post(`/students/${studentId}/FamilyMembers`,updatedFamily);
    await GetStudentFamilyMembers(studentId, dispatch)
    dispatch(closeFamilyForm());

}

export const GetStudentFamilyMembers = async (studentId: number, dispatch: AppDispatch) => {
    const response = await StudentAPI.get<Family[]>(`/students/${studentId}/FamilyMembers`);
    dispatch(setStudentFamilyMembers(response.data));
}

export const GetAllNationalities = async (dispatch: AppDispatch) => {
    const response = await StudentAPI.get<Nationality[]>(`/Nationalities`);
    dispatch(setNationalities(response.data))
}

export const GetFamilyMemberNationality = async (memberId: number): Promise<Family> => {
    const response = await StudentAPI.get<Family>(`/FamilyMembers/${memberId}/Nationality`);
    return response.data;
}

export const UpdateFamilyMemberNationality = async (dispatch: AppDispatch, studentId: number, memberId: number, nationalityId?: number) => {
    await StudentAPI.put(`/FamilyMembers/${memberId}/Nationality/${nationalityId}`);
    await GetStudentFamilyMembers(studentId, dispatch);
    dispatch(closeFamilyForm());
}

export const DeleteFamilyMember = async (dispatch: AppDispatch, studentId: number, memberId: number) => {
    await StudentAPI.delete(`/FamilyMembers/${memberId}`);
    await GetStudentFamilyMembers(studentId, dispatch);
    dispatch(closeFamilyForm());
}
