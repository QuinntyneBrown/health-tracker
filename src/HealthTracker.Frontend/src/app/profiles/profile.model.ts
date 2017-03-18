import { WeightSnapShot } from "./weight-snap-shot.model";

export class Profile { 
    public id:any;
    public name: string;
    public weightSnapShots: Array<WeightSnapShot> = [];
}
