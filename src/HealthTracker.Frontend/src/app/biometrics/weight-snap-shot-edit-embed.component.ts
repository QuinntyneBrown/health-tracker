import { WeightSnapShot } from "./weight-snap-shot.model";
import { EditorComponent } from "../shared";
import { WeightSnapShotDelete, WeightSnapShotEdit, WeightSnapShotAdd } from "./biometrics.actions";

const template = require("./weight-snap-shot-edit-embed.component.html");
const styles = require("./weight-snap-shot-edit-embed.component.scss");

export class WeightSnapShotEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "weight-snap-shot",
            "weight-snap-shot-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.weightSnapShot ? "Edit Weight Snap Shot": "Create Weight Snap Shot";
        this.weighedOnDatePicker = rome(this._weighedOnInputElement, { time: false });

        if (this.weightSnapShot) {                
            this._poundsInputElement.value = this.weightSnapShot.pounds; 
            this._weighedOnInputElement.value = this.weightSnapShot.weighedOn; 
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const weightSnapShot = {
            id: this.weightSnapShot != null ? this.weightSnapShot.id : null,
            pounds: this._poundsInputElement.value,
            weighedOn: this._weighedOnInputElement.value
        } as WeightSnapShot;
        
        this.dispatchEvent(new WeightSnapShotAdd(weightSnapShot));            
    }

    public onDelete() {        
        const weightSnapShot = {
            id: this.weightSnapShot != null ? this.weightSnapShot.id : null,
            pounds: this._poundsInputElement.value,
            weighedOn: this._weighedOnInputElement.value
        } as WeightSnapShot;

        this.dispatchEvent(new WeightSnapShotDelete(weightSnapShot));         
    }
    
    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "weight-snap-shot-id":
                this.weightSnapShotId = newValue;
                break;
            case "weight-snap-shot":
                this.weightSnapShot = JSON.parse(newValue);
                if (this.parentNode) {
                    this.weightSnapShotId = this.weightSnapShot.id;
                    this._poundsInputElement.value = this.weightSnapShot.pounds != undefined ? this.weightSnapShot.pounds : "";
                    this._weighedOnInputElement.value = this.weightSnapShot.weighedOn != undefined ? this.weightSnapShot.weighedOn : "";
                    this._titleElement.textContent = this.weightSnapShotId ? "Edit WeightSnapShot" : "Create WeightSnapShot";
                }
                break;
        }           
    }
    public weightSnapShotId: any;
    public weightSnapShot: WeightSnapShot;
    public weighedOnDatePicker: any;

    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };

    private get _poundsInputElement(): HTMLInputElement { return this.querySelector(".weight-snap-shot-pounds") as HTMLInputElement; }
    private get _weighedOnInputElement(): HTMLInputElement { return this.querySelector(".weight-snap-shot-weighed-on") as HTMLInputElement; }
}

customElements.define(`ce-weight-snap-shot-edit-embed`,WeightSnapShotEditEmbedComponent);
