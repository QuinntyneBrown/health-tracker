const template = require("./weight-snap-shot-master-detail-embed.component.html");
const styles = require("./weight-snap-shot-master-detail-embed.component.scss");

export class WeightSnapShotMasterDetailEmbedComponent extends HTMLElement {
    constructor() {
        super();
    }

    static get observedAttributes () {
        return [];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {

    }

    private _setEventListeners() {

    }

    disconnectedCallback() {

    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            default:
                break;
        }
    }
}

customElements.define(`ce-weight-snap-shot-master-detail-embed`,WeightSnapShotMasterDetailEmbedComponent);
