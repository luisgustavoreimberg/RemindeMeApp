---
name: Slate & Focus
colors:
  surface: '#0b1326'
  surface-dim: '#0b1326'
  surface-bright: '#31394d'
  surface-container-lowest: '#060e20'
  surface-container-low: '#131b2e'
  surface-container: '#171f33'
  surface-container-high: '#222a3d'
  surface-container-highest: '#2d3449'
  on-surface: '#dae2fd'
  on-surface-variant: '#c2c6d6'
  inverse-surface: '#dae2fd'
  inverse-on-surface: '#283044'
  outline: '#8c909f'
  outline-variant: '#424754'
  surface-tint: '#adc6ff'
  primary: '#adc6ff'
  on-primary: '#002e6a'
  primary-container: '#4d8eff'
  on-primary-container: '#00285d'
  inverse-primary: '#005ac2'
  secondary: '#b9c8de'
  on-secondary: '#233143'
  secondary-container: '#39485a'
  on-secondary-container: '#a7b6cc'
  tertiary: '#ffb786'
  on-tertiary: '#502400'
  tertiary-container: '#df7412'
  on-tertiary-container: '#461f00'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#d8e2ff'
  primary-fixed-dim: '#adc6ff'
  on-primary-fixed: '#001a42'
  on-primary-fixed-variant: '#004395'
  secondary-fixed: '#d4e4fa'
  secondary-fixed-dim: '#b9c8de'
  on-secondary-fixed: '#0d1c2d'
  on-secondary-fixed-variant: '#39485a'
  tertiary-fixed: '#ffdcc6'
  tertiary-fixed-dim: '#ffb786'
  on-tertiary-fixed: '#311400'
  on-tertiary-fixed-variant: '#723600'
  background: '#0b1326'
  on-background: '#dae2fd'
  surface-variant: '#2d3449'
  surface-charcoal: '#1e293b'
  border-subtle: '#334155'
  tag-work: '#3b82f6'
  tag-personal: '#22c55e'
  tag-urgent: '#ef4444'
  text-secondary: '#94a3b8'
typography:
  display:
    fontFamily: Inter
    fontSize: 32px
    fontWeight: '700'
    lineHeight: 40px
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Inter
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
  headline-md:
    fontFamily: Inter
    fontSize: 20px
    fontWeight: '600'
    lineHeight: 28px
  body-lg:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  body-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
  label-md:
    fontFamily: JetBrains Mono
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
    letterSpacing: 0.05em
  caption:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '400'
    lineHeight: 16px
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  container-margin: 24px
  gutter: 16px
  stack-sm: 8px
  stack-md: 16px
  stack-lg: 24px
  panel-padding: 20px
---

## Brand & Style

This design system is engineered for high-productivity Windows desktop environments, drawing heavily from the **Modern Corporate** and **Glassmorphism** movements. It prioritizes focus, reduced eye strain, and a professional "utility-first" aesthetic. 

The visual language leverages the Fluent Design philosophy: light, depth, and material. It utilizes translucent layers to maintain context, subtle outlines to define structure without visual noise, and a vibrant accent system to guide the user's attention toward primary actions. The emotional response is one of calm efficiency and technical precision.

## Colors

The palette is built on a foundation of **Deep Slate (#0f172a)** for the application's base layer, providing a stable, low-fatigue environment. **Charcoal (#1e293b)** serves as the elevated surface color for cards, panels, and modals.

**Focus Blue (#3b82f6)** is the primary semantic driver, used for interactive states, primary buttons, and the "Work" category. Secondary colors follow a natural semantic logic: **Green** for personal tasks and **Red** for urgency. Text hierarchy is managed by transitioning from pure white for headings to **Slate-400 (#94a3b8)** for secondary information and metadata.

## Typography

**Inter** is the primary typeface, chosen for its exceptional legibility in desktop software and its neutral, systematic character. It handles high-density data without losing clarity.

For technical metadata, tags, and small utility labels, **JetBrains Mono** is introduced to provide a subtle "developer-friendly" or "precise" feel, echoing the structured nature of a desktop OS. Typography scales are tight and controlled, avoiding massive display sizes to maintain a functional, information-dense workspace.

## Layout & Spacing

The layout follows a **Fluid Grid** model with strict sidebar and header constraints. As a desktop application, it utilizes a 12-column system for content areas but relies heavily on "Panel" logic—where sidebars remain at fixed widths (e.g., 240px or 280px) while the main canvas expands.

A strict 4px/8px baseline grid ensures vertical rhythm. Elements are grouped into logical "Containers" with 20px of internal padding, ensuring that even dense information feels breathable. Margins are consistent at 24px from the window edges to prevent content from feeling cramped against the OS frame.

## Elevation & Depth

Depth is conveyed through **Glassmorphism** and **Tonal Layering** rather than traditional heavy shadows.

- **Level 0 (Background):** Solid Deep Slate (#0f172a).
- **Level 1 (Panels):** Charcoal (#1e293b) with a 1px border (#334155).
- **Level 2 (Modals/Popovers):** Charcoal with 80% opacity and a 20px backdrop-blur. 
- **Outlines:** Every surface must have a 1px subtle border. This "ghost border" technique provides definition in dark mode where shadows are often invisible.
- **Scrollbars:** Custom slim-line scrollbars (6px width) with rounded ends, using Slate-700 for the thumb to blend into the background when not active.

## Shapes

In alignment with Windows 11 aesthetics, the system uses a **Rounded** language. Standard components like buttons and inputs use a 0.5rem (8px) radius, while larger containers like cards and main panels use `rounded-lg` (16px) to create a soft, modern silhouette. Interactive elements should feel approachable but distinct.

## Components

### Buttons
- **Primary:** Solid Focus Blue with white text. High contrast.
- **Secondary:** Transparent background with a #334155 border. On hover, background shifts to a subtle Slate-800.
- **Ghost:** No border or background. Only text/icon. Used for secondary navigation.

### Input Fields
Inputs use the Charcoal surface color with a subtle 1px border. On focus, the border transitions to Focus Blue with a 2px outer glow (0% spread) of the same color.

### Chips & Tags
Tags use a low-opacity background of their semantic color (e.g., 15% Blue for "Work") with a solid color border and text. This ensures they are readable without being visually overwhelming.

### Cards
Cards are the primary organizational unit. They should feature a 1px border (#334155) and a slight elevation transition on hover—increasing the border brightness or adding a very faint ambient glow.

### Lists
List items should have a 4px left-accent "active indicator" in Focus Blue when selected. Hover states should use a subtle highlight (Slate-800) with a 4px corner radius even within a tight list.