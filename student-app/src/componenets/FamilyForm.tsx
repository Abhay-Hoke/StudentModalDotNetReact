import React, {ChangeEvent, useEffect, useState } from 'react'
import styles from '../css/FamilyForm.module.css'
import { useAppDispatch, useAppSelector } from '../app/hooks'
import { closeFamilyForm, setFamilyFormDOB, setFamilyFormFieldValue } from '../features/StudentSlice'
import { AddNewFamilyMember } from '../apis/students'
import DatePicker from 'react-date-picker'
import ReactSelect from 'react-select'

export const FamilyForm = () => {

    const{familyForm, selectedStudentId, relationships, nationalities}= useAppSelector(x =>x.student)

    const dispatch = useAppDispatch();

    const[isFamilyEdit, setIsFamilyEdit] = useState<boolean>(false)
    const nationalitiesOptions = nationalities.map(n => ({label: n.name, value: n.id.toString()}));

    const selectedNationality = nationalitiesOptions.find(n => n.value === familyForm.nationalityId?.toString());


    useEffect(()=>{
        if(familyForm.id>0){
            setIsFamilyEdit(true);
        }
    },[])

    const selectedRelationship = relationships.find(r => r.value === familyForm?.relationship) ?? relationships[0];

    const OnFormChange = (field: string, event: ChangeEvent<HTMLInputElement>) => {
        dispatch(setFamilyFormFieldValue({field, value: event.target.value}))
    }

    const OnFormSelect = (field: string, value: string|number) => {
        dispatch(setFamilyFormFieldValue({field, value}))
    }


    const OnFormDateChange = (value: any) => {
        const date = new Date(value?.toString());
        dispatch(setFamilyFormDOB(date))
    }


    const SubmitFamily= () =>{
        AddNewFamilyMember(selectedStudentId,familyForm,dispatch)
    }

    const onCancelClick =()=>{
        dispatch(closeFamilyForm())
    }

    return (
        <>
            <div className={styles
            ['overlay']}></div>
            <div className={styles['form-wrapper']}>
                <div className={styles['form-title']}>{isFamilyEdit ? 'Edit Family Member' : 'Add New Family Member'}</div>
                <div className={styles['form-element']}>
                    <label htmlFor="fname">First name:</label>
                    <input type="text" id="fname" value={familyForm.firstName} onChange={(e) => OnFormChange('firstName', e)} />
                </div>
                <div className={styles['form-element']}>
                    <label htmlFor="lname">Last name:</label>
                    <input type="text" id="lname" value={familyForm.lastName} onChange={(e) => OnFormChange('lastName', e)} />
                </div>
                <div className={styles['form-element']}>
                    <label htmlFor="lname">Relationship</label>
                    <ReactSelect
                        defaultValue={selectedRelationship}
                        onChange={(e) => OnFormSelect('relationship', e?.value ?? '')}
                        options={relationships}
                        value={selectedRelationship}
                        placeholder='Select Relationship'
                    />
                </div>
                <div className={styles['form-element']}>
                    <label htmlFor="lname">Date of Birth:</label>
                    <DatePicker value={familyForm.dateOfBirth} onChange={OnFormDateChange} />
                </div>
                <div className={styles['form-element']}>
                    <label htmlFor="nationality">Nationality</label>
                    <ReactSelect
                        defaultValue={selectedNationality}
                        onChange={(e) => OnFormSelect('nationalityId', parseInt(e?.value ?? '') ?? 0)}
                        options={nationalitiesOptions}
                        value={selectedNationality}
                        placeholder='Select Nationality'
                    />
                </div>
                <div className={styles['button-wrapper']}>
                    <button className={styles['success-button']} type='button' onClick={SubmitFamily}>{isFamilyEdit ? 'Edit Member' : 'Add Member'}</button>
                    <button className={styles['cancel-button']} type='button' onClick={onCancelClick}>Cancel</button>
                </div>
            </div>
        </>
    )
}

