import { WeightSnapShotAdd, WeightSnapShotDelete, WeightSnapShotEdit, weightSnapShotActions } from "./biometrics.actions";
import { WeightSnapShot } from "./weight-snap-shot.model";

const template = require("./weight-snap-shot-master-detail-embed.component.html");
const styles = require("./weight-snap-shot-master-detail-embed.component.scss");

export class WeightSnapShotMasterDetailEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onWeightSnapShotAdd = this.onWeightSnapShotAdd.bind(this);
        this.onWeightSnapShotEdit = this.onWeightSnapShotEdit.bind(this);
        this.onWeightSnapShotDelete = this.onWeightSnapShotDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "weight-snap-shots"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.weightSnapShotListElement.setAttribute("weight-snap-shots", JSON.stringify(this.weightSnapShots));
    }

    private _setEventListeners() {
        this.addEventListener(weightSnapShotActions.ADD, this.onWeightSnapShotAdd);
        this.addEventListener(weightSnapShotActions.EDIT, this.onWeightSnapShotEdit);
        this.addEventListener(weightSnapShotActions.DELETE, this.onWeightSnapShotDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(weightSnapShotActions.ADD, this.onWeightSnapShotAdd);
        this.removeEventListener(weightSnapShotActions.EDIT, this.onWeightSnapShotEdit);
        this.removeEventListener(weightSnapShotActions.DELETE, this.onWeightSnapShotDelete);
    }

    public onWeightSnapShotAdd(e) {

        const index = this.weightSnapShots.findIndex(o => o.id == e.detail.weightSnapShot.id);
        const indexBaseOnUniqueIdentifier = this.weightSnapShots.findIndex(o => o.weighedOn == e.detail.weightSnapShot.weighedOn);

        if (index > -1 && e.detail.weightSnapShot.id != null) {
            this.weightSnapShots[index] = e.detail.weightSnapShot;
        } else if (indexBaseOnUniqueIdentifier > -1) {
            for (var i = 0; i < this.weightSnapShots.length; ++i) {
                if (this.weightSnapShots[i].weighedOn == e.detail.weightSnapShot.weighedOn)
                    this.weightSnapShots[i] = e.detail.weightSnapShot;
            }
        } else {
            this.weightSnapShots.push(e.detail.weightSnapShot);
        }
        
        this.weightSnapShotListElement.setAttribute("weight-snap-shots", JSON.stringify(this.weightSnapShots));
        this.weightSnapShotEditElement.setAttribute("weight-snap-shot", JSON.stringify(new WeightSnapShot()));
    }

    public onWeightSnapShotEdit(e) {
        this.weightSnapShotEditElement.setAttribute("weight-snap-shot", JSON.stringify(e.detail.weightSnapShot));
    }

    public onWeightSnapShotDelete(e) {
        if (e.detail.weightSnapShot.Id != null && e.detail.weightSnapShot.Id != undefined) {
            this.weightSnapShots.splice(this.weightSnapShots.findIndex(o => o.id == e.detail.optionId), 1);
        } else {
            for (var i = 0; i < this.weightSnapShots.length; ++i) {
                if (this.weightSnapShots[i].weighedOn == e.detail.weightSnapShot.weighedOn)
                    this.weightSnapShots.splice(i, 1);
            }
        }

        this.weightSnapShotListElement.setAttribute("weight-snap-shots", JSON.stringify(this.weightSnapShots));
        this.weightSnapShotEditElement.setAttribute("weight-snap-shot", JSON.stringify(new WeightSnapShot()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "weight-snap-shots":
                this.weightSnapShots = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<WeightSnapShot> { return this.weightSnapShots; }

    private weightSnapShots: Array<WeightSnapShot> = [];
    public weightSnapShot: WeightSnapShot = <WeightSnapShot>{};
    public get weightSnapShotEditElement(): HTMLElement { return this.querySelector("ce-weight-snap-shot-edit-embed") as HTMLElement; }
    public get weightSnapShotListElement(): HTMLElement { return this.querySelector("ce-weight-snap-shot-list-embed") as HTMLElement; }
}

customElements.define(`ce-weight-snap-shot-master-detail-embed`,WeightSnapShotMasterDetailEmbedComponent);
