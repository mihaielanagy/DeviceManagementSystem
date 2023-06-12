export interface User{
    id?: number;
    firstName: string;
    lastName: string;
    email: string;
    role: Role;
    location: Location;
}

export interface Role{
    id: number;
    name: string;
}

export interface Location{
    id: number;
    address: string;
    city: City;
}

export interface City{
    id: number;
    name: string;
    country: Country;
}

export interface Country{
    id: number;
    name: string;
}