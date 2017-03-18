import { WeightSnapShot } from "./weight-snap-shot.model";

export const weightSnapShotActions = {
    ADD: "[WeightSnapShot] Add",
    EDIT: "[WeightSnapShot] Edit",
    DELETE: "[WeightSnapShot] Delete",
};

export class WeightSnapShotEvent extends CustomEvent {
    constructor(eventName:string, weightSnapShot: WeightSnapShot) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { weightSnapShot }
        });
    }
}

export class WeightSnapShotAdd extends WeightSnapShotEvent {
    constructor(weightSnapShot: WeightSnapShot) {
        super(weightSnapShotActions.ADD, weightSnapShot);        
    }
}

export class WeightSnapShotEdit extends WeightSnapShotEvent {
    constructor(weightSnapShot: WeightSnapShot) {
        super(weightSnapShotActions.EDIT, weightSnapShot);
    }
}

export class WeightSnapShotDelete extends WeightSnapShotEvent {
    constructor(weightSnapShot: WeightSnapShot) {
        super(weightSnapShotActions.DELETE, weightSnapShot);
    }
}
