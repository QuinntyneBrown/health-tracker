import { WeightSnapShot } from "./weight-snap-shot.model";

const template = require("./weight-snap-shot-list-embed.component.html");
const styles = require("./weight-snap-shot-list-embed.component.scss");

export class WeightSnapShotListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }
    
    static get observedAttributes() {
        return [
            "weight-snap-shots"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.weightSnapShots.length; i++) {
            let el = this._document.createElement(`ce-weight-snap-shot-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.weightSnapShots[i]));
            this.appendChild(el);
        }    
    }

    weightSnapShots:Array<WeightSnapShot> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "weight-snap-shots":
                this.weightSnapShots = JSON.parse(newValue);
                break;
        }
    }
}

customElements.define("ce-weight-snap-shot-list-embed", WeightSnapShotListEmbedComponent);
