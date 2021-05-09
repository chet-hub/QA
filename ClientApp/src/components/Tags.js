import React from 'react';
import {Badge} from 'reactstrap';

let color_value = [
    "primary", "secondary", "success", "danger", "warning", "info", "light", "dark",
];

export function Tags(props) {
    let tags = props.tags
    return (
        <div>
            {tags.map(tag =>
                <Badge key={tag.id}
                       onClick={(e)=>{
                           e.preventDefault()
                           props.onClick(tag)
                       }}
                       color={color_value[(tag.id * tag.id) % color_value.length]}>
                    <span id={tag.id}>{tag.name}</span>
                </Badge>
            )}
        </div>
    )
}

