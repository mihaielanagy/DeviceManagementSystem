import { User } from "../users/user";

export interface Device{
    id: number;
    name: string;
    manufacturer: Manufacturer;
    deviceType: DeviceType;
    osVersion: OperatingSystemVersion;
    processor : Processor;
    ramAmount : RamAmount;
    user: User;  
}

export interface Manufacturer{
    id: number;
    name: string;
}

export interface DeviceType{
    id: number;
    name: string;
}
export interface OperatingSystemVersion{
    id: number;
    name: string;
    os: OperatingSystem;
}

export interface OperatingSystem{
    id: number;
    name: string;
}
export interface Processor{
    id: number;
    name: string;
}
export interface RamAmount{
    id: number;
    amount: number;
}
