import { useAppDispatch, useAppSelector } from "../app/hooks";
import { addFamily, editStudentDetails } from "../features/StudentSlice";
import { Student } from "../models/Student";
import styles from '../css/StudentList.module.css'
import { GetStudentNationality } from "../apis/students";



export const StudentList = () => {
    const { students, selectedRole } = useAppSelector(x => x.student)
    const dispatch = useAppDispatch();

    const onStudentEdit = async (studentId: number) => {
        const student: Student = await GetStudentNationality(studentId);
        dispatch(editStudentDetails(student));
    }

    const onAddFamilyMember = (studentId: number) => {
      dispatch(addFamily(studentId))
    }

    return <table id={styles['students']}>
    <tr>
      <th>Id</th>
      <th>First Name</th>
      <th>Last Name</th>
      <th>Date of Birth</th>
      <th>Actions</th>
    </tr>
    {
      students.map(student => <tr key={student.id} >
          <td>{student.id}</td>
          <td>{student.firstName}</td>
          <td>{student.lastName}</td>
          <td>{new Date(student.dateOfBirth).toDateString()}</td>
          <td>
            <div className={styles['actions-wrapper']}>
              <div onClick={() => onStudentEdit(student.id)}>{ selectedRole === 'ADMIN' ? 'View': 'Edit'}</div>
              <div onClick={() => onAddFamilyMember(student.id)}>Add Family</div>
            </div>
          </td>
        </tr>)
    }
    
  </table>
}