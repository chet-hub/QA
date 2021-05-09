import React from 'react';
import {convertFromRaw, convertToRaw, Editor, EditorState, SelectionState, Modifier} from 'draft-js';


export default class PlainEditor extends React.Component {
    constructor(props) {
        super(props);
        let value = EditorState.createEmpty();
        if (props.value) {
            value = EditorState.createWithContent(convertFromRaw(props.value))
        }
        this.state = {editorState: value};

        // if (props.setFunction instanceof Function) {
        //     props.setFunction(this.setValue)
        // }

        this.onChange = (editorState) => {
            if (props['onChange'] instanceof Function) {
                let content = this.state.editorState.getCurrentContent();
                let raw = convertToRaw(content);
                props.onChange(raw);
            }
            this.setState({editorState});
        }
        this.logState = () => console.log(this.state.editorState.toJS());
        this.setDomEditorRef = ref => this.domEditor = ref;
        this.focus = () => this.domEditor.focus();
    }

    // setValue = (v) => {
    //     //todo
    // }

    componentDidMount() {
        this.domEditor.focus()
    }

    render() {
        return (
            <div style={styles.root}>
                <div style={styles.editor} onClick={this.focus}>
                    <Editor
                        readOnly={this.props.readOnly}
                        editorState={this.state.editorState}
                        onChange={this.onChange}
                        placeholder=""
                        ref={this.setDomEditorRef}
                    />
                </div>
                {/*<input*/}
                {/*    onClick={this.logState}*/}
                {/*    style={styles.button}*/}
                {/*    type="button"*/}
                {/*    value="Log State"*/}
                {/*/>*/}
            </div>
        );
    }
}

const styles = {
    root: {
        fontFamily: '\'Helvetica\', sans-serif',
        width: '100%'
    },
    editor: {
        border: '1px solid #ccc',
        cursor: 'text',
        minHeight: 80,
        padding: 10,
    },
    button: {
        marginTop: 10,
        textAlign: 'center',
    },
};