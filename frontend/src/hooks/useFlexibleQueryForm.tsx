import { useSearchParams } from "react-router";
import { useCallback, useMemo, useState } from "react";

type AnyFormValues = Record<string, string>;

export function useFlexibleQueryForm<T extends AnyFormValues = AnyFormValues>() {
  const [searchParams, setSearchParams] = useSearchParams();

  const initialValues = useMemo(() => {
    const values: Partial<T> = {};
    for (const [key, value] of searchParams.entries()) {
      values[key as keyof T] = value;
    }
    return values as T;
  }, [searchParams]);

  const [formValues, setFormValues] = useState<T>(() => initialValues);

  const handleChange = (key: keyof T, value: string) => {
    setFormValues(prev => ({ ...prev, [key]: value }));
  };

  const syncFieldToQuery = (key: keyof T) => {
    const newParams = new URLSearchParams(searchParams);
    const value = formValues[key];
    if (value?.trim()) newParams.set(String(key), value.trim());
    else newParams.delete(String(key));
    setSearchParams(newParams);
  };

  const syncAllToQuery = useCallback(() => {
    const newParams = new URLSearchParams(searchParams);
    for (const [key, val] of Object.entries(formValues)) {
      if (val.trim()) newParams.set(key, val.trim());
      else newParams.delete(key);
    }
    setSearchParams(newParams);
  }, [formValues, searchParams, setSearchParams]);

  return {
    formValues,
    setFormValues,
    handleChange,
    syncAllToQuery,
    syncFieldToQuery,
  };
}

