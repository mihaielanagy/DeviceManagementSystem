export interface UserInsert{
    id?: number;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    idRole: number;
    idLocation: number;
}