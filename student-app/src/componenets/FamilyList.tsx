import React, {useEffect} from 'react'
import { DeleteFamilyMember, GetFamilyMemberNationality, GetStudentFamilyMembers } from "../apis/students";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { Family } from '../models/Student';
import { editFamilyDetails } from '../features/StudentSlice';
import styles from '../css/FamilyList.module.css'


interface FamilyListProps {
    studentId: number
  }

 export const FamilyList =(props:FamilyListProps)=>{
    const {studentId} = props;
    const {familyMembers,selectedRole} = useAppSelector(x=>x.student)

    const dispatch = useAppDispatch();
    const isViewOnly = selectedRole === 'ADMIN'
    
    useEffect(() => {
      GetStudentFamilyMembers(studentId, dispatch);
    }, [])
    
    const onFamilyMemberEditClick = async (memberId: number) => {
        const family: Family = await GetFamilyMemberNationality(memberId);
        dispatch(editFamilyDetails(family));
    }

    const onFamilyMemberDeleteClick = async (memberId: number) => {
        await DeleteFamilyMember(dispatch, studentId, memberId) 
      }


    return( 
    <table id={styles['family']}>
    <tr>
      <th>Id</th>
      <th>First Name</th>
      <th>Last Name</th>
      <th>Date of Birth</th>
      {
        !isViewOnly && <th>Actions</th>
      }
    </tr>
    {
      familyMembers.map(family => <tr key={family.id} >
          <td>{family.id}</td>
          <td>{family.firstName}</td>
          <td>{family.lastName}</td>
          <td>{new Date(family.dateOfBirth).toDateString()}</td>
          {
            !isViewOnly && 
            <td>
              <div className={styles['actions-wrapper']}>
                <div onClick={() => onFamilyMemberEditClick(family.id)}>Edit</div>
                <div onClick={() => onFamilyMemberDeleteClick(family.id)}>Delete</div>
              </div>
            </td>
          }
        </tr>)
    }
    
  </table>
    )
}