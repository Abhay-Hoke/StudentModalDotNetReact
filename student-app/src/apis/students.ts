import axios,{AxiosInstance} from "axios"
import { Family, Nationality, Student } from "../models/Student"
import { AppDispatch } from "../app/store"
import { closeFamilyForm, closeStudentForm, setNationalities, setStudentFamilyMembers, setStudents } from "../features/StudentSlice"


export const createModuleAPI =(baseURL:string):
AxiosInstance => {
    const instance = axios.create({baseURL})
    return instance
}

export const StudentAPI = createModuleAPI(`http://localhost:5151/api`)

export const AddNewStudent =async (student: Student,
    dispatch:AppDispatch)=>{
        const updatedStudent ={
            ...student,
            nationalityId:null
        }
    
    if(student.id > 0) {
        const data = await StudentAPI.put(`/Students/${student.id}`,updatedStudent);
        await UpdateStudentNationality(dispatch, student.id, student.nationalityId);
     } else {
         const response = await StudentAPI.post<Student>('/Students',updatedStudent);
        await UpdateStudentNationality(dispatch, response.data.id, student.nationalityId);
 
     }
     await GetAllStudents(dispatch);
    dispatch(closeStudentForm());
}

export const GetStudentNationality = async (studentId: number): Promise<Student> => {
    const response = await StudentAPI.get<Student>(`/Students/${studentId}/Nationality`);
    return response.data;
}

export const UpdateStudentNationality = async (dispatch: AppDispatch, studentId: number, nationalityId?: number) => {
    await StudentAPI.put(`/Students/${studentId}/Nationality/${nationalityId}`);
    await GetAllStudents(dispatch);
    dispatch(closeStudentForm());
}

export const GetAllStudents = async (dispatch: AppDispatch) => {
    const response = await StudentAPI.get<Student[]>('/Students');
    if(response) {
        dispatch(setStudents(response.data))
    }
}

export const AddNewFamilyMember = async (studentId: number, family: Family, dispatch: AppDispatch) => {
    if(family.id > 0) {
        await StudentAPI.put<Family>(`/FamilyMembers/${family.id}`,family);
        if(family?.nationalityId && family?.nationalityId > 0) {
            await StudentAPI.put(`/FamilyMembers/${family.id}/Nationality/${family.nationalityId}`);
        }
        await GetStudentFamilyMembers(family.studentId, dispatch)
    } else {
        const updatedFamily = {
            ...family,
            nationalityId: null,
            studentId
        }
        const familyResponse = await StudentAPI.post<Family>(`/Students/${studentId}/FamilyMembers`,updatedFamily);
        const familyDetails : Family = familyResponse.data;
        if(family?.nationalityId && family?.nationalityId > 0) {
            await StudentAPI.put(`/FamilyMembers/${familyDetails.id}/Nationality/${family.nationalityId}`);
        }
        await GetStudentFamilyMembers(studentId, dispatch)
    }
    dispatch(closeFamilyForm());

}

export const GetStudentFamilyMembers = async (studentId: number, dispatch: AppDispatch) => {
    const response = await StudentAPI.get<Family[]>(`/Students/${studentId}/FamilyMembers`);
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

// export const UpdateFamilyMemberNationality = async (dispatch: AppDispatch, studentId: number, memberId: number, nationalityId?: number) => {
//     await StudentAPI.put(`/FamilyMembers/${memberId}/Nationality/${nationalityId}`);
//     await GetStudentFamilyMembers(studentId, dispatch);
//     dispatch(closeFamilyForm());
// }

export const DeleteFamilyMember = async (dispatch: AppDispatch, studentId: number, memberId: number) => {
    await StudentAPI.delete(`/FamilyMembers/${memberId}`);
    await GetStudentFamilyMembers(studentId, dispatch);
    dispatch(closeFamilyForm());
}