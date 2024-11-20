export interface Student {
    id: number,
    firstName: string,
    lastName: string,
    dateOfBirth: Date
    nationalityId?: number
}

export interface Family extends Student{
    relationshipId: number
    relationship: string
}

export interface Nationality {
    id: number,
    country: string
}