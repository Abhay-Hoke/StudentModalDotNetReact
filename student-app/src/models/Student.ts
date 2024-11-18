export interface Student {
    id: number,
    firstName: string,
    lastName: string,
    dateOfBirth: Date
    nationalityId?: number
    [key: string]: any;
    // key problem in slicer
}

export interface Family extends Student{
    studentId: number
    relationship: string
}

export interface Nationality {
    id: number,
    name: string
}