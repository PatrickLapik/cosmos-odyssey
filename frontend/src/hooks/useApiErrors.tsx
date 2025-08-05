import type { FieldValues, Path, UseFormSetError } from "react-hook-form";

type BackendErrors = {
  errors: Record<string, string[]>;
};

export const useApiErrors = () => {
  function handleErrors<T extends FieldValues>(
    errorResponse: BackendErrors,
    setError: UseFormSetError<T>,
  ) {
    const fieldErrors = errorResponse.errors;
    for (const field in fieldErrors) {
      const messages = fieldErrors[field];
      if (messages.length > 0) {
        setError(field === "" ? "root" : field as Path<T>, {
          type: "server",
          message: messages[0],
        });
      }
    }
  }

  return {
    handleErrors,
  };
};
