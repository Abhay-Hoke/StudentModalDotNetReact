import React from 'react';
import Select from 'react-select';
import styles from './Header.module.css';
import { useAppSelector } from '../app/hooks.ts';
import { useDispatch } from 'react-redux';
import { addNewStudent, setSelectedRole } from '../features/StudentSlice.ts';

export const Header = () => {
    const { roles, selectedRole} = useAppSelector(x => x.student)
    const selectedRoleForSelect = roles.find(r => r.value === selectedRole);
    const dispatch = useDispatch()

    const onSelectChange = (e) => {
        dispatch(setSelectedRole(e.value))
    }
    const onAddStudentClick = (e) => {
        dispatch(addNewStudent())
    }

    return <div className={styles['header-wrapper']}>
        <div className={styles['header-title']}>Students Portal</div>
        <div className={styles['header-right-section']}>
            <button onClick={onAddStudentClick}>Add Student</button>
            <div className={styles['select-wrapper']}>
                <Select 
                defaultValue={selectedRoleForSelect}
                onChange={onSelectChange}
                options={roles}
                value={selectedRoleForSelect}
                placeholder='Select Role'
                />
            </div>
        </div>
    </div>
}