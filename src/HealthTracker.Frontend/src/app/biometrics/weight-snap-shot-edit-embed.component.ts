import { WeightSnapShot } from "./weight-snap-shot.model";
import { EditorComponent } from "../shared";
import {  WeightSnapShotDelete, WeightSnapShotEdit, WeightSnapShotSave } from "./weight-snap-shot.actions";

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

        if (this.weightSnapShot) {                
            this._nameInputElement.value = this.weightSnapShot.name;  
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
            name: this._nameInputElement.value
        } as WeightSnapShot;
        
        this.dispatchEvent(new WeightSnapShotSaveEvent(weightSnapShot));            
    }

    public onDelete() {        
        const weightSnapShot = {
            id: this.weightSnapShot != null ? this.weightSnapShot.id : null,
            name: this._nameInputElement.value
        } as WeightSnapShot;

        this.dispatchEvent(new WeightSnapShotDeleteEvent(weightSnapShot));         
    }

    public weightSnapShot: WeightSnapShot;

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "weight-snap-shot-id":
                this.weightSnapShotId = newValue;
                break;
            case "weightSnapShot":
                this.weightSnapShot = JSON.parse(newValue);
                if (this.parentNode) {
                    this.weightSnapShotId = this.weightSnapShot.id;
                    this._nameInputElement.value = this.weightSnapShot.name != undefined ? this.weightSnapShot.name : "";
                    this._titleElement.textContent = this.weightSnapShotId ? "Edit WeightSnapShot" : "Create WeightSnapShot";
                }
                break;
        }           
    }

    public weightSnapShot: WeightSnapShot;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".weight-snap-shot-name") as HTMLInputElement;}
}

customElements.define(`ce-weight-snap-shot-edit-embed`,WeightSnapShotEditEmbedComponent);
