import React from 'react';

const Card = ({
    children,
    className = '',
    padding = 'md',
    hover = false,
    onClick,
}) => {
    const paddings = {
        sm: 'p-3',
        md: 'p-6',
        lg: 'p-8',
        none: 'p-0',
    };

    const hoverEffect = hover ? 'hover:shadow-lg transition-shadow duration-200 cursor-pointer' : '';

    return (
        <div
            className={`bg-white rounded-lg shadow-md ${paddings[padding]} ${hoverEffect} ${className}`}
            onClick={onClick}
        >
            {children}
        </div>
    );
};

export default Card;