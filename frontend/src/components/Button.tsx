import React from 'react'
import clsx from 'clsx'

type ButtonVariant = 'primary' | 'secondary' | 'outlined' | 'no_bg_underline'

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: ButtonVariant
}

const baseClasses =
  'px-4 py-2 rounded-lg font-medium transition-colors duration-200 cursor-pointer'

const variantClasses: Record<ButtonVariant, string> = {
  primary: 'bg-blue-950 text-white hover:bg-blue-900',
  secondary: 'text-white bg-gray-900 hover:bg-gray-800',
  outlined:
    'bg-transparent border border-gray-600 text-gray-200 hover:bg-zinc-900',
  no_bg_underline: 'hover:font-semibold',
}

const Button: React.FC<ButtonProps> = ({
  children,
  variant = 'primary',
  className,
  disabled,
  ...props
}) => {
  return (
    <button
      className={clsx(
        baseClasses,
        variantClasses[variant],
        {
          'opacity-50 cursor-not-allowed': disabled,
        },
        className
      )}
      disabled={disabled}
      {...props}
    >
      {children}
    </button>
  )
}

export default Button
