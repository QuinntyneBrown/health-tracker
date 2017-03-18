import { WeightSnapShot } from "../biometrics";

export class Profile { 
    public id:any;
    public name: string;
    public weightSnapShots: Array<WeightSnapShot> = [];
}
