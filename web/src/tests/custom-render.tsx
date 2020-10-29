import React, { ReactElement } from "react";
import { render, RenderOptions, RenderResult } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";

const Wrapper : React.FC = ({ children }) => {
    return (
        <MemoryRouter>{children}</MemoryRouter>
    );
  };

function renderAll(
    ui: ReactElement,
    options?: Omit<RenderOptions, 'queries'>): RenderResult {
    return render(ui, { ...options, wrapper: Wrapper});
}

export * from "@testing-library/react";
export { renderAll };