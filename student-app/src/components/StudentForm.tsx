import React, { ChangeEvent, useEffect, useState } from 'react';
import styles from './StudentForm.module.css';
import { useAppDispatch, useAppSelector } from '../app/hooks.ts';
import { addFamily, closeStudentForm, setStudentFormDOB, setStudentFormFieldValue } from '../features/StudentSlice.ts';
import DatePicker from 'react-date-picker';
import 'react-date-picker/dist/DatePicker.css';
import 'react-calendar/dist/Calendar.css';
import { AddNewStudent, UpdateStudentNationality } from '../apis/students.ts';
import Select from 'react-select';
import { FamilyList } from './FamilyList.tsx';

export const StudentForm = () => {
    const {studentForm, nationalities, selectedRole} = useAppSelector(x => x.student)
    const dispatch = useAppDispatch();
    const [isEditStudent, setIsEditStudent] = useState<boolean>(false)
    const nationalitiesOptions = nationalities.map(n => ({label: n.country, value: n.id.toString()}));
    const selectedNationality = nationalitiesOptions.find(n => n.value === studentForm.nationalityId?.toString());
    const isViewOnly = selectedRole === 'ADMIN' && isEditStudent
    useEffect(() => {
        if(studentForm.id > 0) {
            setIsEditStudent(true);
        }
    }, [])

    const OnFormChange = (field: string, event: ChangeEvent<HTMLInputElement>) => {
        dispatch(setStudentFormFieldValue({field, value: event.target.value}))
    }

    const OnFormSelect = (field: string, value: number) => {
        dispatch(setStudentFormFieldValue({field, value}))
    }

    const OnFormDateChange = (value: any) => {
        const date = new Date(value?.toString());
        dispatch(setStudentFormDOB(date))
    }

    const SubmitStudent = () => {
        AddNewStudent(studentForm, dispatch)
    }

    const SubmitStudentNationality = () =>{
        UpdateStudentNationality(dispatch,studentForm.id, studentForm.nationalityId);
    }

    const onCancelClick = () => {
        dispatch(closeStudentForm())
    }

    const onAddFamilyMember = (studentId: number) => {
        dispatch(addFamily(studentId))
      }

    return <>
    <div className={styles['overlay']}></div>
    <div className={styles['form-wrapper']}>
        <div className={styles['form-title']}>{isEditStudent ? 'Edit Student' : 'Add New Student'}</div>
        <div className={styles['form-row']}>
            <div className={styles['form-element']}>
                <label htmlFor="fname">First name:</label>
                <input type="text" id="fname" 
                value={studentForm.firstName} 
                onChange={(e) => OnFormChange('firstName', e)}
                disabled={isViewOnly}
                />
            </div>
            <div className={styles['form-element']}>
                <label htmlFor="lname">Last name:</label>
                <input type="text" id="lname" value={studentForm.lastName} 
                onChange={(e) => OnFormChange('lastName', e)}
                disabled={isViewOnly}
                />
            </div>
        </div>
        <div className={styles['form-row']}>
            <div className={styles['form-element']}>
                <label htmlFor="lname">Date of Birth:</label>
                <DatePicker value={studentForm.dateOfBirth} 
                onChange={OnFormDateChange} 
                disabled={isViewOnly}
                
                />
            </div>
            <div className={styles['form-element']}>
                <label htmlFor="nationality">Nationality</label>
                <Select 
                    defaultValue={selectedNationality}
                    onChange={(e) => OnFormSelect('nationalityId', parseInt(e?.value ?? '') ?? 0)}
                    options={nationalitiesOptions}
                    value={selectedNationality}
                    placeholder='Select Nationality'
                    isDisabled={isViewOnly}

                />
            </div>
        </div>
        {
            studentForm.id > 0 && <>
            <div className={styles['family-member-title-wrapper']}>
                <div className={styles['family-members-title']}>Family Members</div>
                <button className={styles['success-button']} type='button' 
                onClick={() => onAddFamilyMember(studentForm.id)}>{'Add Family'}</button>
            </div>
             <FamilyList studentId={studentForm.id}/>
            </>
        }
        <div className={styles['button-wrapper']}>
        {
            !isViewOnly &&
            <button className={styles['success-button']} type='button' onClick={SubmitStudent}>{isEditStudent ? 'Edit Student': 'Add Student'}</button>
        }
        {
            isEditStudent && !isViewOnly && <button className={styles['success-button']} type='button' onClick={SubmitStudentNationality}>Update Nationality</button>
        }
        <button className={styles['cancel-button']} type='button' onClick={onCancelClick}>Cancel</button>
        </div>
    </div>
    </>
}