import React from 'react';
import ReactSelect from 'react-select'
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { addNewStudent, setSelectedRole } from "../features/StudentSlice";
import styles from '../css/Header.module.css'
import Select from 'react-select/dist/declarations/src/Select';

export const Header = () => {
    const { roles, selectedRole} = useAppSelector(x => x.student)
    const selectedRoleForSelect = roles.find(r => r.value === selectedRole);
    const dispatch = useAppDispatch()

    //e type problem
    const onSelectChange = (e:any) => {
        dispatch(setSelectedRole(e.value))
    }
    const onAddStudentClick = (e:any) => {
        dispatch(addNewStudent())
    }

    return(
        <div className={styles['header-wrapper']}>
        <div className={styles['header-title']}>Students Portal</div>
        <div className={styles['header-right-section']}>
            <button onClick={onAddStudentClick}>Add Student</button>
            <div className={styles['select-wrapper']}>
                <ReactSelect 
                defaultValue={selectedRoleForSelect}
                onChange={onSelectChange}
                options={roles}
                value={selectedRoleForSelect}
                placeholder='Select Role'
                />
            </div>
        </div>
    </div>
    )
}